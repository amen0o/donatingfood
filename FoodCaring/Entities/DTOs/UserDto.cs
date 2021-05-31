using Entities.Models;
using System.Collections.Generic;
using System.Linq;

namespace Entities.DTOs
{
    public class UserDto
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
        public ICollection<OrderDto> Orders { get; set; }
        public int PriorityComputed { get; set; }
        public ICollection<FoodIntoleranceDto> FoodIntolerances { get; set; }

        public UserDto() { }

        public UserDto(User user)
        {
            Id = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Email = user.UserName;
            Orders = user.Orders;
            PriorityComputed = user.PriorityComputed;
            Role = user.Role;
            FoodIntolerances = user.UserFoodIntolerances?
                .Where(y => y.FoodIntolerance != null)
                .Select(x => new FoodIntoleranceDto
                {
                    Id = x.FoodIntolerance.Id,
                    Name = x.FoodIntolerance?.Name
                }).ToList();
        }
    }
}
