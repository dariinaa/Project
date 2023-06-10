using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace Project.Controllers.RecipeAPI
{
    public class ExtendedIngredient
    {
        [DataMember(Name = "name", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "name")]
        public string name { get; set; }

        [DataMember(Name = "amount", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "amount")]
        public double amount { get; set; }

        [DataMember(Name = "unit", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "unit")]
        public string unit { get; set; }

    }
}
