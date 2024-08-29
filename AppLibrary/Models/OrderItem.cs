using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLibrary.Models;

public class OrderItem : BaseEntity
{
    //public int OrderId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public DateTime Date { get; private set; } = DateTime.Now;
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    //public Order? Order { get; set; }
    public Product Product { get; set; } = null!;
}