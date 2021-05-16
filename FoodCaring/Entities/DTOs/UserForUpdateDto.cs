using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entities.DTOs
{
    public class UserForUpdateDto
    {
        [Required(ErrorMessage = "FirstName is required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "LastName is required")]
        public string LastName { get; set; }
        public string Email { get; set; }
        [Required(ErrorMessage = "Username is required")]
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
