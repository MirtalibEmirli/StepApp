using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLibrary.Models;

public class LikedItem : BaseEntity
{
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    public string? Items { get; set; }

}
