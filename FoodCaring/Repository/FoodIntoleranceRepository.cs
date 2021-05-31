using Entities;
using Entities.Models;

namespace Repository
{
    public class FoodIntoleranceRepository : RepositoryBase<FoodIntolerance>
    {
        public FoodIntoleranceRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }
    }
}