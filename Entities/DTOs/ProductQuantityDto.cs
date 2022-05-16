using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class ProductQuantityDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public short BarKodNo { get; set; }
        public short QuantityOrder { get; set; }
        public short QuantityReceive { get; set; }

    }
}
