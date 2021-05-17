using Entities.DTOs;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public UserController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetAll()
        {
            var users = _userManager.Users.ToList();

            return Ok(users);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetUser(Guid id)
        {
            var user = _userManager.Users.FirstOrDefault(x => x.Id == id.ToString());

            var roles = await _userManager.GetRolesAsync(user);
            user.Role = string.Join(",", roles);

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
    }
}
