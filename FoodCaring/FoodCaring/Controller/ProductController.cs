using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Repository;
using Microsoft.EntityFrameworkCore;
using Entities.DTOs;

namespace FoodCaring.Controller
{
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly RepositoryManager _repositoryManager;

        public ProductController(RepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        [HttpPost("create")]
        [Authorize()]
        public async Task<IActionResult> CreateProduct([FromBody] ProductToUpdateDto productToAdd)
        {
            if (productToAdd is null || string.IsNullOrEmpty(productToAdd.Title))
            {
                return BadRequest("Invalid product");
            }

            var product = new Product()
            {
                Image = productToAdd.Image,
                Price = productToAdd.Price,
                Title = productToAdd.Title,
                Restaurant = new Restaurant { Id = productToAdd.RestaurantId }
            };

            _repositoryManager.Product.CreateProduct(product);

            await _repositoryManager.SaveAsync();

            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult GetProduct(int id)
        {
            var product = _repositoryManager.Product.FindByCondition(x => x.Id.Equals(id))
                .Include(x => x.Restaurant).FirstOrDefault();

            if (product == null)
            {
                return NoContent();
            }

            return Ok(product);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var products = _repositoryManager.Product.FindAll().Include(x => x.Restaurant);

            return Ok(products);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product =
                _repositoryManager.Product.FindByCondition(x => x.Id.Equals(id)).FirstOrDefault();

            if (product == null)
            {
                return NoContent();
            }

            _repositoryManager.Product.Delete(product);

            await _repositoryManager.SaveAsync();

            return Ok();
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Update(int id, [FromBody] ProductToUpdateDto productToUpdate)
        {
            var existingProduct = _repositoryManager.Product
                .FindByCondition(x => x.Id == id).FirstOrDefault();

            if (existingProduct == null)
            {
                return BadRequest($"Product with id {id} not found");
            }

            var restaurant = _repositoryManager.Restaurant
                .FindByCondition(x => x.Id == productToUpdate.RestaurantId)
                .FirstOrDefault();

            if (restaurant == null)
            {
                return BadRequest($"Product.Restaurant with id {productToUpdate.RestaurantId} not found");
            }

            existingProduct.Restaurant = restaurant;
            existingProduct.Title = productToUpdate.Title;
            existingProduct.Price = productToUpdate.Price;
            existingProduct.Image = productToUpdate.Image;

            _repositoryManager.Product.Update(existingProduct);

            await _repositoryManager.SaveAsync();

            return NoContent();
        }
    }
}
