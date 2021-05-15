using AutoMapper;
using CompanyEmployees.ActionFilters;
using Contracts;
using Entities.DTOs;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace FoodCaring.Controller
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IAuthenticationManager _authManager;
        public AuthenticationController(IMapper mapper, UserManager<User> userManager, IAuthenticationManager authManager)
        {
            _mapper = mapper;
            _userManager = userManager;
            _authManager = authManager;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDto userForRegistration)
        {
            //var user = _mapper.Map<User>(userForRegistration);
            var user = new User()
            {
                FirstName = userForRegistration.FirstName,
                LastName = userForRegistration.LastName,
                UserName = userForRegistration.UserName,
                PasswordHash = userForRegistration.Password,
                Email = userForRegistration.Email
            };

            var result = await _userManager.CreateAsync(user, userForRegistration.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }

                return BadRequest(ModelState);
            }

            if (!userForRegistration.Roles.Any())
            {
                await _userManager.AddToRoleAsync(user, "Manager");
            }
            else
            {
                await _userManager.AddToRolesAsync(user, userForRegistration.Roles);
            }

            return StatusCode(201);
        }

        [HttpPost("login")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> Authenticate([FromBody] UserForAuthenticationDto user)
        {
            if (!await _authManager.ValidateUser(user))
            {
                return Unauthorized();
            }

            return Ok(new { Token = await _authManager.CreateToken() });
        }

        [HttpPost("test")]
        public IActionResult Test()
        {
            return Content("Reusit");
        }
        
        [HttpPost("test2")]
        [Authorize]
        public IActionResult Test2()
        {
            return Content("Reusit2");
        }
    }
}
