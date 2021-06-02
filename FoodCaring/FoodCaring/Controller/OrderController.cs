using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Repository;
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Entities.DTOs;

namespace FoodCaring.Controller
{
    [Route("api/order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly RepositoryManager _repositoryManager;
        private readonly UserManager<User> _userManager;

        public OrderController(RepositoryManager repositoryManager,
            UserManager<User> userManager)
        {
            _repositoryManager = repositoryManager;
            _userManager = userManager;
        }

        [HttpPost("create")]
        [Authorize]
        public async Task<IActionResult> CreateOrder([FromBody] Order order)
        {
            if (order == null || order.OrderItems == null || !order.OrderItems.Any())
            {
                return BadRequest("Invalid Order");
            }

            // TODO: reset user priority

            order.OrderDate = DateTime.Now;
            order.OrderNumber = Guid.NewGuid().ToString();
            order.IsFinalized = false;

            foreach (var item in order.OrderItems)
            {
                item.UnitPrice = item.Product.Price;
            }

            _repositoryManager.Order.CreateOrder(order);

            await _repositoryManager.SaveAsync();

            return Ok();
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetOrder(string userId)
        {
            var currentOrder = _repositoryManager.Order
                .FindAll(false)
                .Include(x => x.User)
                .Include(x => x.OrderItems)
                    .ThenInclude(x => x.Product)
                .FirstOrDefault(x => x.IsFinalized == false && x.User != null && x.User.Id == userId);

            if (currentOrder == null)
            {
                currentOrder = new Order
                {
                    OrderItems = new List<OrderItem>(),
                    User = _userManager.Users.FirstOrDefault(x => x.Id == userId.ToString()),
                    OrderNumber = Guid.NewGuid().ToString(),
                    IsFinalized = false
                };

                _repositoryManager.Order.CreateOrder(currentOrder);

                await _repositoryManager.SaveAsync();
            }

            currentOrder.CalculateTotal();

            return Ok(currentOrder);
        }

        [HttpPost("placeOrder")]
        public async Task<IActionResult> PlaceOrder([FromBody] PlaceOrderDto orderToPlace)
        {
            if (orderToPlace == null || orderToPlace.OrderId <= 0)
            {
                return BadRequest("Invalid place order object");
            }

            var orderToUpdate = _repositoryManager.Order.FindAll()
                .FirstOrDefault(x => x.Id == orderToPlace.OrderId);

            orderToUpdate.IsFinalized = true;
            orderToUpdate.TargetUser = _userManager.Users
                .FirstOrDefault(x => x.Id == orderToPlace.TargetUserId);

            _repositoryManager.Order.Update(orderToUpdate);
            await _repositoryManager.SaveAsync();

            return Ok();
        }
    }
}
