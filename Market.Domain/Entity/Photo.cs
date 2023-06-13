using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Domain.Entity
{
    public class Photo
    {
        public long Id { get; set; }
        public byte[] ImageData { get; set; }
        public long ProductId { get; set; }
        public Product Product { get; set; }
    }
}
