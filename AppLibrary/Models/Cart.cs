using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLibrary.Models
{
    public class Cart : BaseEntity
    {
        public int UserId { get; set; }
        public List<Product> Items { get; set; }
        public User User { get; set; }

    }
}
