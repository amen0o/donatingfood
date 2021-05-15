using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class OrderItem
    {
        [Column("OrderItemId")]
        public int Id { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public float UnitPrice { get; set; }
        public Order Order { get; set; }
    }
}