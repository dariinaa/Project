using Project.Data.DataModels;
using System.ComponentModel.DataAnnotations;

namespace Project.Services.ViewModels
{
    public class RecipeCategoryViewModel
    {
        [Key]
        public string? RecipeCategoryId { get; set; }
        public string RecipeCategoryName { get; set; }
        public string RecipeCategoryImage { get; set; }

        public ICollection<Recipe>? RecipeCategoryRecipes { get; set; }
    }
}
