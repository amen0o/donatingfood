using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class OrderRepository : RepositoryBase<Order>
    {
        private readonly OrderItemRepository orderItemRepository;

        public OrderRepository(RepositoryContext repositoryContext, OrderItemRepository orderItemRepository) : base(repositoryContext)
        {
            this.orderItemRepository = orderItemRepository;
        }

        public void CreateOrder(Order order)
        {
            Create(order);
            RepositoryContext.Entry(order.User).State = EntityState.Unchanged;
            foreach(var orderItem in order.OrderItems)
            {
                orderItemRepository.CreateOrderItem(orderItem);
            }
        }
    }
}
