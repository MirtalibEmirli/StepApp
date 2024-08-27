using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLibrary.Models
{
    public class Product : BaseEntity
    {
        public string Name { get; set; } = null!;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;
        public PhotoProduct? Photo { get; set; }
        public IEnumerable<Order> Orders { get; set; } = [];
        //public List<Cart> Carts { get; set; } = [];
        public List<CartProduct> CartItems { get; set; } = [];

    }
}
