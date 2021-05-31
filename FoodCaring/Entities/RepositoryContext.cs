using Entities.Configuration;
using Entities.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Entities
{
    public class RepositoryContext : IdentityDbContext<User>
    {
        public RepositoryContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new OrderItemConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new RestaurantConfiguration());
            modelBuilder.ApplyConfiguration(new FoodIntoleranceConfiguration());

            modelBuilder.Entity<UserFoodIntolerance>()
                .HasKey(uf => new { uf.UserId, uf.FoodIntoleranceId });
            modelBuilder.Entity<UserFoodIntolerance>()
                .HasOne(bc => bc.User)
                .WithMany(c => c.UserFoodIntolerances)
                .HasForeignKey(bc => bc.UserId);
            modelBuilder.Entity<UserFoodIntolerance>()
                .HasOne(bc => bc.FoodIntolerance)
                .WithMany(c => c.UserFoodIntolerances)
                .HasForeignKey(bc => bc.FoodIntoleranceId);
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<FoodIntolerance> FoodIntolerances { get; set; }
    }
}
