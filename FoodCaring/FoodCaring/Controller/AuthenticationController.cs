using AutoMapper;
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
using Repository;

namespace FoodCaring.Controller
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IAuthenticationManager _authManager;
        private readonly RepositoryManager _repositoryManager;

        public AuthenticationController(IMapper mapper, UserManager<User> userManager, IAuthenticationManager authManager,
            RepositoryManager repositoryManager)
        {
            _mapper = mapper;
            _userManager = userManager;
            _authManager = authManager;
            _repositoryManager = repositoryManager;
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
            AddOrders(loggedInUser);
            
            var userDto = new UserDto(loggedInUser);

            userDto.Token = await _authManager.CreateToken();
            userDto.Role = string.Join(",", await _userManager.GetRolesAsync(loggedInUser));

            return Ok(userDto);
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
