using System;

namespace Entities.Models
{
    public class UserFoodIntolerance
    {
        public string UserId { get; set; }
        public User User { get; set; }
        public int FoodIntoleranceId { get; set; }
        public FoodIntolerance FoodIntolerance { get; set; }
    }
}
