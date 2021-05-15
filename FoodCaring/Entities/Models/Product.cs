using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Product
    {
        [Column("ProductId")]
        public int Id { get; set; }
        public Restaurant Restaurant { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public float Price { get; set; }
    }
}
