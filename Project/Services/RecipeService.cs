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
                RecipeId = recipe.RecipeId,
                RecipeTitle = recipe.RecipeTitle,
                RecipeDescription = recipe.RecipeDescription,
                RecipeAuthor = recipe.RecipeAuthor,

            }).ToList();
        }
        public RecipeViewModel GetDetailsById(string recipeId)
        {
            RecipeViewModel recipe = context.Recipe
                .Select(recipe => new RecipeViewModel
                {
                    
                    RecipeId = recipe.RecipeId,
                    RecipeTitle = recipe.RecipeTitle,
                    RecipeVideo = recipe.RecipeVideo,
                    RecipeInredients = recipe.RecipeInredients,
                    RecipeDescription = recipe.RecipeDescription,
                    RecipeAuthor = recipe.RecipeAuthor,
                    RecipeIntroduction = recipe.RecipeIntroduction,
                    RecipeDirections = recipe.RecipeDirections,
                    RecipeCookTime = recipe.RecipeCookTime,
                    RecipeCalories = recipe.RecipeCalories,
                    RecipeServings = recipe.RecipeServings,
                }).SingleOrDefault(recipe => recipe.RecipeId == recipeId);

            return recipe;
        }
        public async Task AddRecipe(RecipeViewModel recipe)
        {
            var recipeDb = new Recipe();
            recipeDb.RecipeId = recipe.RecipeId;
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
        public RecipeViewModel UpdateById(string recipeId)
        {
            RecipeViewModel recipe = context.Recipe
                .Select(recipe => new RecipeViewModel
                {
                    RecipeId = recipe.RecipeId,
                    RecipeTitle = recipe.RecipeTitle,
                    RecipeVideo = recipe.RecipeVideo,
                    RecipeInredients = recipe.RecipeInredients,
                    RecipeDescription = recipe.RecipeDescription,
                    RecipeAuthor = recipe.RecipeAuthor,
                    RecipeIntroduction = recipe.RecipeIntroduction,
                    RecipeDirections = recipe.RecipeDirections,
                    RecipeCookTime = recipe.RecipeCookTime,
                    RecipeCalories = recipe.RecipeCalories,
                    RecipeServings = recipe.RecipeServings,
                }).SingleOrDefault(recipe => recipe.RecipeId == recipeId);

            return recipe;
        }
        public async Task UpdateAsync(RecipeViewModel model)
        {
            Recipe recipe = context.Recipe.Find(model.RecipeId);

            bool isRecipeNull = recipe == null;
            if (isRecipeNull)
            {
                return;
            }
            recipe.RecipeId = model.RecipeId;
            recipe.RecipeTitle = model.RecipeTitle;
            recipe.RecipeVideo = model.RecipeVideo;
            recipe.RecipeInredients = model.RecipeInredients;
            recipe.RecipeDescription = model.RecipeDescription;
            recipe.RecipeAuthor = model.RecipeAuthor;
            recipe.RecipeIntroduction = model.RecipeIntroduction;
            recipe.RecipeDirections = model.RecipeDirections;
            recipe.RecipeCookTime = model.RecipeCookTime;
            recipe.RecipeCalories = model.RecipeCalories;
            recipe.RecipeServings = model.RecipeServings;
            context.Recipe.Update(recipe);
            await context.SaveChangesAsync();
        }
    }
}
