using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class ProductDto
    {
        public int ProductId { get; set; }
        public string CompanyName { get; set; }
        public string ProductName { get; set; }
        public short UnitPrice { get; set; }
        public DateTime ExprationDate { get; set; }
        //public DateTime SK { get; set; }
    }
}
