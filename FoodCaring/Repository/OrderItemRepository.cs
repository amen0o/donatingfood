using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;

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

        internal void UpdateOrderItem(OrderItem orderItem)
        {
            RepositoryContext.Entry(orderItem.Product).State = EntityState.Unchanged;
            RepositoryContext.Entry(orderItem.Order).State = EntityState.Unchanged;
            Update(orderItem);
        }
    }
}
