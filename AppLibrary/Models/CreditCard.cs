using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLibrary.Models
{
    public class CreditCard : BaseEntity
    {

        public string Number { get; set; } = null!;
        public DateTime ExpirationDate { get; set; }
        public string CVV { get; set; } = null!;
        public int UserId { get; set; }
        public User User { get; set; } = null!;
    }
}
