using Entities.DTOs;
using Entities.Models;
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
        //[Authorize]
        public async Task<IActionResult> GetAll()
        {
            var users = _userManager.Users.Include(x => x.UserFoodIntolerances).ToList();

            foreach (var user in users)
            {
                await AddUserRoles(user);
                AddOrders(user);

                user.PriorityComputed = user.Orders.Count() + user.Priority;
                //user.FoodIntolerances = user.UserFoodIntolerances.Select(x => x.FoodIntolerance).ToList();
            }

            return Ok(users);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetUser(Guid id)
        {
            var user = _userManager.Users.Include(x => x.UserFoodIntolerances)
                .FirstOrDefault(x => x.Id == id.ToString());

            await AddUserRoles(user);
            //user.FoodIntolerances = user.UserFoodIntolerances.Select(x => x.FoodIntolerance).ToList();

            return Ok(user);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
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
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UserForUpdateDto userToUpdate)
        {
            var existingUser = _userManager.Users.FirstOrDefault(x => x.Id == id.ToString());

            if (existingUser == null)
            {
                return BadRequest($"User with id {id} not found");
            }

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

        [HttpPost("updateIntolerances/{id}")]
        public async Task<IActionResult> UpdateIntolerances(Guid id, [FromBody] UpdateIntolerancesDto updateIntolerancesDto)
        {
            if (updateIntolerancesDto == null || updateIntolerancesDto.IntolerancesIds == null
                || !updateIntolerancesDto.IntolerancesIds.Any() )
            {
                return BadRequest($"{nameof(updateIntolerancesDto.IntolerancesIds)} is null or empty");
            }

            var existingUser = _userManager.Users.Include(x => x.UserFoodIntolerances)
                .FirstOrDefault(x => x.Id == id.ToString());

            if (existingUser == null)
            {
                return BadRequest($"User with id {id} not found");
            }

            var intolerances = _repositoryManager.FoodIntolerance
                .FindByCondition(x => updateIntolerancesDto.IntolerancesIds.Contains(x.Id))
                .ToList();

            existingUser.UserFoodIntolerances = intolerances.Select(x =>
                new UserFoodIntolerance()
                {
                    FoodIntoleranceId = x.Id,
                    UserId = existingUser.Id
                }).ToList();

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
