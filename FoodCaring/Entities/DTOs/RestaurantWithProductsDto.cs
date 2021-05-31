using Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.DTOs
{
    public class RestaurantWithProductsDto
    {
        [Required]
        public int RestaurantId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public ICollection<Product> Products { get; set; }
    }
}
