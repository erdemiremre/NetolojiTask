using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Order :IEntity
    {
        public int OrderId { get; set; }
        public int SupplierId { get; set; }
        public int EmployeeId { get; set; }
        public int ProductId { get; set; }
        public short UnitPrice { get; set; }
        public short Quantity { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime RequiredDate { get; set; }
    }
}
