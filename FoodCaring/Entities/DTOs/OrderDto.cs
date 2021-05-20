using Entities.Models;
using System.Collections.Generic;

namespace Entities.DTOs
{
    public class OrderDto
    {
        public int Id { get; set; }
        public ICollection<OrderItemDto> OrderItems { get; set; }
    }
}
