using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities.Configuration
{
    public class FoodIntoleranceConfiguration : IEntityTypeConfiguration<FoodIntolerance>
    {
        public void Configure(EntityTypeBuilder<FoodIntolerance> builder)
        {
            builder.HasData(
              new FoodIntolerance
              {
                  Id = 1,
                  Name = "Gluten"
              },
              new FoodIntolerance
              {
                  Id = 2,
                  Name = "Lactose",
              },
              new FoodIntolerance
              {
                  Id = 3,
                  Name = "Caffeine",
              },
              new FoodIntolerance
              {
                  Id = 4,
                  Name = "Sulfites",
              },
              new FoodIntolerance
              {
                  Id = 5,
                  Name = "Fructose",
              },
              new FoodIntolerance
              {
                  Id = 6,
                  Name = "Salicylates",
              }
            );
        }
    }
}
