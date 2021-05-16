using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Repository;

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
                return BadRequest(new { error = "Restaurant is invalid"});
            }

            _repositoryManager.Restaurant.Create(restaurant);
            await _repositoryManager.SaveAsync();

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var restaurantCollection = _repositoryManager.Restaurant.FindAll(false);

            await _repositoryManager.SaveAsync();

            return Ok(restaurantCollection);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRestaurant(int id)
        {
            var restaurant =
                _repositoryManager.Restaurant.FindByCondition(x => x.Id.Equals(id), false).FirstOrDefault();

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
