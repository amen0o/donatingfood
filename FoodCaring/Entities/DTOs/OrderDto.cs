using Entities.Models;
using System.Collections.Generic;
using System.Linq;

namespace Entities.DTOs
{
    public class OrderDto
    {
        public int Id { get; set; }
        public float Total { get; private set; }
        public ICollection<OrderItemDto> OrderItems { get; set; }
        public bool IsFinalized { get; set; }

        public OrderDto()
        {

        }

        public OrderDto(Order order)
        {
            Id = order.Id;
            Total = order.Total;
            OrderItems = order.OrderItems.Select(x => new OrderItemDto(x)).ToList();
            IsFinalized = order.IsFinalized;
        }
    }
}
