using Entities.Models;

namespace Entities.DTOs
{
    public class OrderItemDto
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public float UnitPrice { get; set; }

        public OrderItemDto(OrderItem orderItem)
        {
            Id = orderItem.Id;
            Product = orderItem.Product;
            Quantity = orderItem.Quantity;
            UnitPrice = orderItem.UnitPrice;
        }
    }
}
