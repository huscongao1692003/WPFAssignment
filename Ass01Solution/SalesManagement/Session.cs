using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagement
{
    public class Session
    {
        public static int? userId { get; set; } = null;
        public static List<OrderDetail> carts { get; set; } = null;
    }
}
