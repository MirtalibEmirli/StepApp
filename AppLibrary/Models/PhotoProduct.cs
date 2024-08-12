using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLibrary.Models
{

    public class PhotoProduct : BaseEntity
    {
        public byte[] Bytes { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string FileExtension { get; set; } = null!;
        public decimal Size { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;
    }
}
