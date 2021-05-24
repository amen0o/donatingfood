using Entities.Models;
using System.ComponentModel.DataAnnotations;

namespace Entities.DTOs
{
    public class ProductToUpdateDto
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Image { get; set; }
        [Required]
        public float Price { get; set; }
        [Required]
        public int RestaurantId { get; set; }
    }
}
