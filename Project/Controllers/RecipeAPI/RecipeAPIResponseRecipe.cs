using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace Project.Controllers.RecipeAPI
{
    public class RecipeAPIResponseRecipe
    {
        [DataMember(Name = "title", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "title")]
        public string title { get; set; }

        [DataMember(Name = "image", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "image")]
        public string image { get; set; }

        [DataMember(Name = "servings", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "servings")]
        public int servings { get; set; }

        [DataMember(Name = "readyInMinutes", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "readyInMinutes")]
        public int readyInMinutes { get; set; }

        [DataMember(Name = "instructions", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "instructions")]
        public string instructions { get; set; }

        [DataMember(Name = "summary", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "summary")]
        public string summary { get; set; }

        [DataMember(Name = "extendedIngredients", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "extendedIngredients")]
        public List<ExtendedIngredient> extendedIngredients { get; set; }
    }
}
