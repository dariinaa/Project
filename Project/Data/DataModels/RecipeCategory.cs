using System.ComponentModel.DataAnnotations;

namespace Project.Data.DataModels
{
    public class RecipeCategory
    {
        [Key]
        public string RecipeCategoryId { get; set; }
        public string RecipeCategoryName { get; set; }
        public string RecipeCategoryImage { get; set; }

        public ICollection<Recipe> RecipeCategoryRecipes { get; set; }
    }
}
