using Entities.DTOs;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [NotMapped]
        public string Role { get; set; }
        [NotMapped]
        public string Token { get; set; }
        [NotMapped]
        public ICollection<OrderDto> Orders { get; set; }
        public int Priority { get; set; }

        [NotMapped]
        public int PriorityComputed { get; set; }
    }
}
