using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersService
{
    class OrdersObj
    {
        public int count { get; set; }
        public List<Order> results { get; set; }
    }
}
