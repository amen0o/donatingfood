using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class OrderItemRepository : RepositoryBase<OrderItem>

    {
        public OrderItemRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        internal void CreateOrderItem(OrderItem orderItem)
        {
            Create(orderItem);
            RepositoryContext.Entry(orderItem.Product).State = EntityState.Unchanged;
        }
    }
}
