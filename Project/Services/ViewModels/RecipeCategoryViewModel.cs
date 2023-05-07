using Project.Data.DataModels;

namespace Project.Services.ViewModels
{
    public class RecipeCategoryViewModel
    {
        public string RecipeCategoryId { get; set; }
        public string RecipeCategoryName { get; set; }

        public ICollection<Recipe> RecipeCategoryRecipes { get; set; }
    }
}
