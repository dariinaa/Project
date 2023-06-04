using System.ComponentModel.DataAnnotations;

namespace Project.Data.DataModels
{
    public class Recipe
    {
        [Key]
        public string RecipeId { get; set; }
        public string RecipeTitle { get; set; }
        public string RecipeImage { get; set; }
        public string RecipeInredients { get; set; }
        public string RecipeDescription { get; set; }
        public string RecipeIntroduction { get; set; }
        public string RecipeDirections { get; set; }
        public double RecipeCookTime { get; set; }
        public double RecipeCalories { get; set; }
        public int RecipeServings { get; set; }

        public string? RecipeAuthorId { get; set; }
        public User User { get; set; }

        public string? RecipeCategoryId { get; set; }
        public RecipeCategory RecipeCategory { get; set; }

        public string? CuisineId { get; set; }
        public Cuisine Cuisine { get; set; }

        public string? ReviewId { get; set; }
        public ICollection<Review> RecipeReviews { get; set; }
    }
}
