using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class Restaurant
    {
        [Column("RestaurantId")]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
