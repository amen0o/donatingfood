using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Repository;

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
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> CreateProduct([FromBody] Product product)
        {
            if (product is null || string.IsNullOrEmpty(product.Title))
            {
                return BadRequest(new { error = "Product has no name."});
            }

            _repositoryManager.Product.CreateProduct(product);

            await _repositoryManager.SaveAsync();

            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = _repositoryManager.Product.FindByCondition(x => x.Id.Equals(id), false).FirstOrDefault();

            if (product == null)
            {
                return NoContent();
            }

            await _repositoryManager.SaveAsync();

            return Ok(product);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var productCollection = _repositoryManager.Product.FindAll(false);

            return Ok(productCollection);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product =
                _repositoryManager.Product.FindByCondition(x => x.Id.Equals(id), false).FirstOrDefault();

            if (product == null)
            {
                return NoContent();
            }

            _repositoryManager.Product.Delete(product);

            await _repositoryManager.SaveAsync();

            return Ok();
        }
    }
}
