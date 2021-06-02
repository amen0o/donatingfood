using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Repository;
using Entities.DTOs;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace FoodCaring.Controller
{
    [Route("api/restaurant")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private readonly RepositoryManager _repositoryManager;

        public RestaurantController(RepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        [HttpPost("create")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> CreateRestaurant([FromBody] Restaurant restaurant)
        {
            if (restaurant is null || string.IsNullOrEmpty(restaurant.Name))
            {
                return BadRequest(new { error = "Restaurant is invalid" });
            }

            _repositoryManager.Restaurant.Create(restaurant);
            await _repositoryManager.SaveAsync();

            return Ok();
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var restaurantCollection = _repositoryManager.Restaurant.FindAll();

            return Ok(restaurantCollection);
        }

        [HttpGet("all")]
        public IActionResult GetAllWithProducts()
        {
            var restaurantCollection = _repositoryManager.Restaurant.FindAll().ToList();
            var productCollection = _repositoryManager.Product.FindAll().Include(x => x.Restaurant).ToList();

            var restaurants = new List<RestaurantWithProductsDto>();

            foreach (var rest in restaurantCollection)
            {
                var restaurant = new RestaurantWithProductsDto
                {
                    RestaurantId = rest.Id,
                    Name = rest.Name,
                    Products = new List<Product>()
                };

                foreach (var prod in productCollection)
                {
                    if (prod.Restaurant.Id == restaurant.RestaurantId)
                    {
                        restaurant.Products.Add(prod);
                    }
                }

                restaurants.Add(restaurant);
            }

            return Ok(restaurants);
        }

        [HttpGet("{id}")]
        public IActionResult GetRestaurant(int id)
        {
            var restaurant = _repositoryManager.Restaurant.FindByCondition(x => x.Id.Equals(id)).FirstOrDefault();

            if (restaurant == null)
            {
                return NoContent();
            }

            return Ok(restaurant);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRestaurant(int id)
        {
            var restaurant =
                _repositoryManager.Restaurant.FindByCondition(x => x.Id.Equals(id)).FirstOrDefault();

            if (restaurant == null)
            {
                return NoContent();
            }

            _repositoryManager.Restaurant.Delete(restaurant);

            await _repositoryManager.SaveAsync();

            return Ok();
        }
    }
}
