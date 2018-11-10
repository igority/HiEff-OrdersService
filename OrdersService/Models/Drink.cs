using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersService
{
    class Drink
    {
        [JsonProperty(PropertyName = "id")]
        public int id { get; set; }
        [JsonProperty(PropertyName = "ingredients")]
        public List<Ingredient> ingredients { get; set; }
        [JsonProperty(PropertyName = "prep")]
        public int prep { get; set; }
        [JsonProperty(PropertyName = "glass")]
        public Glass glass { get; set; }
        [JsonProperty(PropertyName = "ice")]
        public int ice { get; set; }
        [JsonProperty(PropertyName = "garnishes")]
        public List<string> garnishes { get; set; }
        [JsonProperty(PropertyName = "quantity")]
        public int quantity { get; set; }
    }
}
