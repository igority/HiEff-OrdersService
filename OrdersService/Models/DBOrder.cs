using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersService
{
    class DBOrder
    {
        //todo - as they are in the plc 
        public int ID { get; set; }
        public int iStatus { get; set; }
        public int iTray_number { get; set; }
        public int status { get; set; }
        public int tray_number { get; set; }
        public List<Product> products { get; set; }
        public string qr_code { get; set; }

        public int count { get; set; }
    }
}
