using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLibrary.Models
{
    public class User : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public byte[] Media { get; set; }
        public string Password { get; set; }
        public string CreditCards { get; set; }
        public Cart Cart { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
