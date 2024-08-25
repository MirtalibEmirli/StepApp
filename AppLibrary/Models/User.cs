using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLibrary.Models
{
    public class User : BaseEntity
    {
        public string Firstname { get; set; } = null!;
        public string LatsName { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public DateOnly DateofBirth { get; set; }
        public string Email { get; set; } = null!;
        
        public IEnumerable<Order> History { get; } = [];
        public byte[] Password { get; set; } = null!;
        public byte[] Photo { get; set; } = null!;
        public IEnumerable<CreditCard> CreditCards { get; } = [];
    }
}
