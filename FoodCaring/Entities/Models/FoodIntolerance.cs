using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class FoodIntolerance
    {
        [Column("FoodIntoleranceId")]
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<UserFoodIntolerance> UserFoodIntolerances { get; set; }
    }
}
