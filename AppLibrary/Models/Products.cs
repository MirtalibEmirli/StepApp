using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLibrary.Models
{
    public class Product : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public string Desc { get; set; }
        public int CategoryId { get; set; }
        public string Media { get; set; }

        public Category Category { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
