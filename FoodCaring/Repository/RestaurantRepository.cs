using Entities;
using Entities.Models;

namespace Repository
{
    public class RestaurantRepository : RepositoryBase<Restaurant>
    {
        public RestaurantRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
            
        }
    }
}
