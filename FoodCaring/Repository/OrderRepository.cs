using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

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
            foreach (var orderItem in order.OrderItems)
            {
                orderItemRepository.CreateOrderItem(orderItem);
            }
        }

        public void AddOrUpdateOrderItem(Order currentOrder, OrderItem newOrderItem)
        {
            var existingOrderItem = currentOrder.OrderItems.FirstOrDefault(x => x.Product.Id == newOrderItem.Product.Id);
            if (existingOrderItem == null)
            {
                orderItemRepository.CreateOrderItem(newOrderItem);
                currentOrder.OrderItems.Add(newOrderItem);
            }
            else
            {
                existingOrderItem.Quantity += newOrderItem.Quantity;
                orderItemRepository.UpdateOrderItem(existingOrderItem);
            }
            RepositoryContext.Entry(currentOrder.User).State = EntityState.Unchanged;
            Update(currentOrder);
        }
    }
}
