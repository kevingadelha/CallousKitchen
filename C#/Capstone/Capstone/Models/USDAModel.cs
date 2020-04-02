using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Models
{
    
    public class USDAModel
    {
        [JsonProperty(PropertyName = "description")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "gtinUpc")]
        public string Code { get; set; }
    }
}