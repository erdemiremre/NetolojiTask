using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.AdoNet
{
    public class Context
    {
        public string Conn { get; set; }
        public Context()
        {
            Conn = ConfigurationManager.ConnectionStrings["DbConnectionString"].ConnectionString;
        }
    }
}
