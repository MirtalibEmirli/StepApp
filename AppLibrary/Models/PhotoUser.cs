using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLibrary.Models
{

    public class PhotoUser : BaseEntity
    {
        public byte[] Bytes { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string FileExtension { get; set; } = null!;
        public decimal Size { get; set; }
        public int UserId { get; set; }
        public User User { get; set; } = null!;
    }
}
