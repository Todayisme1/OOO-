using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOO_Восток.Classes
{
    public class Helper
    {
        public static Model.DBVostok DB { get; set; }

        public static Model.Users users { get; set; }

        public static List<Classes.ProductInOrder> productInOrders = new List<Classes.ProductInOrder>();
    }
}
