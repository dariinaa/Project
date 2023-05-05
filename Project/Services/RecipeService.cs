using Project.Data;
using Project.Data.DataModels;
using Project.Services.ViewModels;

namespace Project.Services
{
    public class RecipeService
    {
        private readonly ApplicationDbContext context;
        public RecipeService(ApplicationDbContext post)
        {
            context = post;
        }
        public List<RecipeViewModel> GetAll()
        {
            return context.Recipe.Select(recipe => new RecipeViewModel()
            {
                RecipeTitle = recipe.RecipeTitle,
                RecipeDescription = recipe.RecipeDescription,
                RecipeAuthor = recipe.RecipeAuthor,

            }).ToList();
        }
        public async Task AddRecipe(RecipeViewModel recipe)
        {
            var recipeDb = new Recipe();
            recipeDb.RecipeTitle = recipe.RecipeTitle;
            recipeDb.RecipeVideo = recipe.RecipeVideo;
            recipeDb.RecipeInredients = recipe.RecipeInredients;
            recipeDb.RecipeDescription = recipe.RecipeDescription;
            recipeDb.RecipeAuthor = recipe.RecipeAuthor;
            recipeDb.RecipeIntroduction = recipe.RecipeIntroduction;
            recipeDb.RecipeDirections = recipe.RecipeDirections;
            recipeDb.RecipeCookTime = recipe.RecipeCookTime;
            recipeDb.RecipeCalories = recipe.RecipeCalories;
            recipeDb.RecipeServings = recipe.RecipeServings;
            if (recipe.RecipeTitle != null)
            {
                context.Add(recipeDb);
                await context.SaveChangesAsync();
            }
            else
            {
                Console.WriteLine("Eror!");
            }
        }
        public async Task DeleteRecipe(string id)
        {
            if (string.IsNullOrEmpty(id) || string.IsNullOrWhiteSpace(id))
            {
                Console.WriteLine("Eror!");
            }
            if (id != null)
            {
                var recipeDb = context.Recipe.FirstOrDefault(x => x.RecipeId.ToString() == id);
                context.Recipe.Remove(recipeDb);
                await context.SaveChangesAsync();
            }
        }
        public async Task UpdateRecipe(string id, RecipeViewModel recipe)
        {
            if (string.IsNullOrEmpty(id) || string.IsNullOrWhiteSpace(id))
            {
                Console.WriteLine("Eror!");
            }
            if (id != null)
            {
                var recipeDb = context.Recipe.FirstOrDefault(x => x.RecipeId.ToString() == id);
                recipeDb.RecipeTitle = recipe.RecipeTitle;
                recipeDb.RecipeVideo = recipe.RecipeVideo;
                recipeDb.RecipeInredients = recipe.RecipeInredients;
                recipeDb.RecipeDescription = recipe.RecipeDescription;
                recipeDb.RecipeAuthor = recipe.RecipeAuthor;
                recipeDb.RecipeIntroduction = recipe.RecipeIntroduction;
                recipeDb.RecipeDirections = recipe.RecipeDirections;
                recipeDb.RecipeCookTime = recipe.RecipeCookTime;
                recipeDb.RecipeCalories = recipe.RecipeCalories;
                recipeDb.RecipeServings = recipe.RecipeServings;
                await context.SaveChangesAsync();
            }
        }
    }
}
