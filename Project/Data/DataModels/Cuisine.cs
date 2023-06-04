using System.ComponentModel.DataAnnotations;

namespace Project.Data.DataModels
{
    public class Cuisine
    {
        [Key]
        public string CuisineId { get; set; }
        public string CuisineName { get; set; }
        public string CuisineImage { get; set; }

        public ICollection<Recipe> CuisineRecipes { get; set; }
    }
}
