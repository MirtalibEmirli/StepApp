using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLibrary.Models
{
    public class CreditCard : BaseEntity
    {
        public decimal money { get; set; }
        public int UserId { get; set; }
        public User User { get; set; } = null!;
    }
}
