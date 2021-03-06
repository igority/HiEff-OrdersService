﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersService
{
    class Order
    {
        [JsonProperty(PropertyName = "id")]
        public int id { get; set; }
        [JsonProperty(PropertyName = "status")]
        public int status { get; set; }
        [JsonProperty(PropertyName = "tray_number")]
        public int tray_number { get; set; }
        [JsonProperty(PropertyName = "products")]
        public List<Product> products { get; set; }
        [JsonProperty(PropertyName = "qr_code")]
        public string qr_code { get; set; }

        public int count { get; set; }
    }
}
