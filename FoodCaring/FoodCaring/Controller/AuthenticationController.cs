﻿using AutoMapper;
using CompanyEmployees.ActionFilters;
using Contracts;
using Entities.DTOs;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

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

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDto userForRegistration)
        {
            var user = new User()
            {
                FirstName = userForRegistration.FirstName,
                LastName = userForRegistration.LastName,
                UserName = userForRegistration.UserName,
                PasswordHash = userForRegistration.Password,
                Email = userForRegistration.Email ?? userForRegistration.UserName
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

            if (string.IsNullOrEmpty(userForRegistration.Role))
            {
                await _userManager.AddToRoleAsync(user, "Donator");
            }
            else
            {
                await _userManager.AddToRolesAsync(user, new List<string> { userForRegistration.Role });
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

            var loggedInUser = _userManager.Users
                .Include(x => x.UserFoodIntolerances)
                .FirstOrDefault(x => x.UserName == user.UserName.ToString());

            var userDto = new UserDto(loggedInUser);

            userDto.Token = await _authManager.CreateToken();
            userDto.Role = string.Join(",", await _userManager.GetRolesAsync(loggedInUser));

            return Ok(userDto);
        }
    }
}
