using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Models
{
    public class FoodFactsProductModel
    {
        [JsonProperty(PropertyName = "product_name_en")]
        public string Name { get; set; }
    }
}