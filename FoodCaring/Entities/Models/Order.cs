using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class Order
    {
        [Column("OrderId")]
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderNumber { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        [ForeignKey("TargetUserId")]
        public virtual User TargetUser { get; set; }
        public bool IsFinalized { get; set; }

        [NotMapped]
        public float Total { get; private set; }

        public void CalculateTotal()
        {
            float total = 0;
            foreach(var item in OrderItems)
            {
                total += item.UnitPrice * item.Quantity;
            }

            Total = total;
        }
    }
}