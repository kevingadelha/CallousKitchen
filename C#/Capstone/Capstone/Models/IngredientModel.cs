using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Capstone.Models
{
 /*   // Author Peter Szadurski // depricated class
    public class IngredientModel
    {
        [JsonProperty(PropertyName = "text")]
        public string Text { get; set; }
        [JsonProperty(PropertyName = "weight")]
        public float Weight { get; set; }
        [JsonProperty(PropertyName = "image")]
        public float Image { get; set; }
    }
    [DataContract]
    [Serializable]
    public class SerializableIngredientModel
    {
        [DataMember]
        public string Text { get; set; }
        [DataMember]
        public float Weight { get; set; }
        [DataMember]
        public float Image { get; set; }

        public SerializableIngredientModel (IngredientModel m)
        {
            Text = m.Text;
            Weight = m.Weight;
            Image = m.Image;
        }
    }
 */
}