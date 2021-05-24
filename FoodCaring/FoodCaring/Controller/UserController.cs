using Entities.DTOs;
using Entities.Models;
using FoodCaring.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodCaring.Controller
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly RepositoryManager _repositoryManager;

        public UserController(UserManager<User> userManager, RepositoryManager repositoryManager)
        {
            _userManager = userManager;
            _repositoryManager = repositoryManager;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var users = _userManager.Users.ToList();
            foreach (var user in users)
            {
                await AddUserRoles(user);
                AddOrders(user);

                user.PriorityComputed = user.Orders.Count() + user.Priority;
            }

            return Ok(users);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetUser(Guid id)
        {
            var user = _userManager.Users.FirstOrDefault(x => x.Id == id.ToString());
            await AddUserRoles(user);

            return Ok(user);
        }

        [HttpDelete("{id}")]
        // TODO: uncomment
        //[Authorize(Roles = "Administrator")] 
        public async Task<IActionResult> Delete(Guid id)
        {
            var user = _userManager.Users.FirstOrDefault(x => x.Id == id.ToString());

            if (user != null)
            {
                var result = await _userManager.DeleteAsync(user);
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.TryAddModelError(error.Code, error.Description);
                    }

                    return BadRequest(ModelState);
                }

                return NoContent();
            }

            return BadRequest($"User with id {id} not found");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UserForUpdateDto userToUpdate)
        {
            var existingUser = _userManager.Users.FirstOrDefault(x => x.Id == id.ToString());

            if (existingUser != null)
            {
                existingUser.FirstName = userToUpdate.FirstName;
                existingUser.LastName = userToUpdate.LastName;

                var result = await _userManager.UpdateAsync(existingUser);
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.TryAddModelError(error.Code, error.Description);
                    }

                    return BadRequest(ModelState);
                }

                if (!string.IsNullOrEmpty(userToUpdate.Role))
                {
                    var roles = await _userManager.GetRolesAsync(existingUser);
                    await _userManager.RemoveFromRolesAsync(existingUser, roles);
                    await _userManager.AddToRolesAsync(existingUser, new List<string> { userToUpdate.Role });
                }

                return NoContent();
            }

            return BadRequest($"User with id {id} not found");
        }

        [HttpPut("modifyPriority/{id}/{priorityModifier}")]
        public async Task<IActionResult> IncreasePriority(Guid id, int priorityModifier)
        {
            var existingUser = _userManager.Users.FirstOrDefault(x => x.Id == id.ToString());

            if (existingUser == null)
            {
                return BadRequest($"User with id {id} not found");
            }

            existingUser.Priority += priorityModifier;

            var result = await _userManager.UpdateAsync(existingUser);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }

                return BadRequest(ModelState);
            }

            return NoContent();
        }

        private async Task AddUserRoles(User user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            user.Role = string.Join(",", roles);
        }

        private void AddOrders(User user)
        {
            var orders = _repositoryManager.Order
                .FindByCondition(x => x.User.Id == user.Id, trackChanges: false)
                .Include(y => y.OrderItems)
                .ToList();

            user.Orders = new List<OrderDto>();
            foreach (var order in orders)
            {
                user.Orders.Add(new OrderDto()
                {
                    Id = order.Id,
                    OrderItems = order.OrderItems.Select(x => new OrderItemDto(x)).ToList()
                });
            }
        }
    }
}
