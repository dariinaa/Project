using Project.Data.DataModels;
using System.ComponentModel.DataAnnotations;

namespace Project.Services.ViewModels
{
    public class IngredientViewModel
    {
        [Key]
        public int IngredientId { get; set; }

        public string IngredientName { get; set; }

        public string IngredientQuantity { get; set; }

        public string RecipeId { get; set; }
        public Recipe Recipe { get; set; }
    }
}
