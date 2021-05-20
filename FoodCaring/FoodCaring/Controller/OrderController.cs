using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Repository;
using System;

namespace FoodCaring.Controller
{
    [Route("api/order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly RepositoryManager _repositoryManager;

        public OrderController(RepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
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

            foreach(var item in order.OrderItems)
            {
                item.UnitPrice = item.Product.Price;
            }

            _repositoryManager.Order.CreateOrder(order);

            await _repositoryManager.SaveAsync();

            return Ok();
        }
    }
}
