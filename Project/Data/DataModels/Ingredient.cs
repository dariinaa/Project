using System.ComponentModel.DataAnnotations;

namespace Project.Data.DataModels
{
    public class Ingredient
    {
        [Key]
        public int IngredientId { get; set; }

        public string IngredientName { get; set; }

        public string IngredientQuantity { get; set; }

        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }
    }
}
