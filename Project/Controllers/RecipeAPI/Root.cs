using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace Project.Controllers.RecipeAPI
{
    public class Root
    {
        [DataMember(Name = "recipes")]
        [JsonProperty(PropertyName = "recipes")]
        public List<RecipeAPIResponseRecipe> recipes { get; set; }
    }
}
