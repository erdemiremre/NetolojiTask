using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
     public class ProductDetailDto:ProductDto
    {
        public short BarKodNo { get; set; }
        public short ShelfLife { get; set; }
        public DateTime StockInDate { get; set; }
    }
}
