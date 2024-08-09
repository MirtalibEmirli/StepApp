using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLibrary.Models
{
     
        public class Photo:BaseEntity
        {
             
            public byte[] Bytes { get; set; }
            public string Description { get; set; }
            public string FileExtension { get; set; }
            public decimal Size { get; set; }
            public int ProductId { get; set; }
            public Product Product { get; set; }
        }
     
}
