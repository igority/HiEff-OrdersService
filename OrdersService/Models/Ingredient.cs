using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersService
{
    class Ingredient
    {
        [JsonProperty(PropertyName = "ingredient")]
        public IngredientDetail ingredient { get; set; }
        [JsonProperty(PropertyName = "ratio_ml")]
        public int ratio_ml { get; set; }
    }
}
