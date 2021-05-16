using Entities;
using Entities.Models;

namespace Repository
{
    public class ProductRepository : RepositoryBase<Product>
    {
        public ProductRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public void CreateProduct(Restaurant restaurant, Product product)
        {
            product.Restaurant = restaurant;
            Create(product);
        }
    }
}
