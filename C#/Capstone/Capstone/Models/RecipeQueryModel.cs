using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Hits
    {
        [JsonProperty(PropertyName = "recipe")]
        public RecipeModel Recipe { get; set; }
    }
    public class RecipeQueryModel
    {


        [JsonProperty(PropertyName = "q")]
        public string Query { get; set; }
        [JsonProperty(PropertyName = "hits")]

        public Hits[] Hits { get; set; }

    }
}