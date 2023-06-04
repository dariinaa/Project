using Microsoft.EntityFrameworkCore;
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

        //get all e
        public List<RecipeViewModel> GetAll()
        {
            return context.Recipe.Select(recipe => new RecipeViewModel()
            {
                RecipeId = recipe.RecipeId,
                RecipeTitle = recipe.RecipeTitle,
                RecipeImage = recipe.RecipeImage,
                RecipeDescription = recipe.RecipeDescription,
            }).ToList();
        }

        //getDetailsById
        public RecipeViewModel GetDetailsById(string recipeId)
        {
            RecipeViewModel recipe = context.Recipe
                .Select(recipe => new RecipeViewModel
                {                   
                    RecipeId = recipe.RecipeId,
                    RecipeTitle = recipe.RecipeTitle,
                    RecipeImage = recipe.RecipeImage,
                    RecipeInredients = recipe.RecipeInredients,
                    RecipeDescription = recipe.RecipeDescription,
                    User = recipe.User,
                    RecipeIntroduction = recipe.RecipeIntroduction,
                    RecipeDirections = recipe.RecipeDirections,
                    RecipeCookTime = recipe.RecipeCookTime,
                    RecipeCalories = recipe.RecipeCalories,
                    RecipeServings = recipe.RecipeServings,
                }).SingleOrDefault(recipe => recipe.RecipeId == recipeId);

            return recipe;
        }

        //addRecipe
        /*public async Task AddRecipe(RecipeViewModel recipe)
        {
            var recipeDb = new Recipe();
            recipeDb.RecipeId = recipe.RecipeId;
            recipeDb.RecipeTitle = recipe.RecipeTitle;
            recipeDb.RecipeImage = recipe.RecipeImage;
            recipeDb.RecipeInredients = recipe.RecipeInredients;
            recipeDb.RecipeDescription = recipe.RecipeDescription;
            recipeDb.RecipeIntroduction = recipe.RecipeIntroduction;
            recipeDb.RecipeDirections = recipe.RecipeDirections;
            recipeDb.RecipeCookTime = recipe.RecipeCookTime;
            recipeDb.RecipeCalories = recipe.RecipeCalories;
            recipeDb.RecipeServings = recipe.RecipeServings;
            recipeDb.RecipeAuthorId = recipe.RecipeAuthorId;
            recipeDb.RecipeCategoryId = recipe.RecipeCategoryId;
            recipeDb.CuisineId = recipe.CuisineId;
            if (recipe.RecipeTitle != null)
            {
                context.Add(recipeDb);
                await context.SaveChangesAsync();
            }
            else
            {
                Console.WriteLine("Eror!");
            }
        }*/

        public async Task AddRecipe(RecipeViewModel recipe)
        {
            var author = await context.Users.FirstOrDefaultAsync(u => u.UserName == recipe.RecipeAuthorId);
            var category = await context.RecipeCategory.FirstOrDefaultAsync(rc => rc.RecipeCategoryName == recipe.RecipeCategoryId);
            var cuisine = await context.Cuisine.FirstOrDefaultAsync(c => c.CuisineName == recipe.CuisineId);

            if (author == null || category == null || cuisine == null)
            {
                throw new Exception("Invalid author, category, or cuisine name.");
            }
            var recipeDb = new Recipe
            {
                RecipeId = Guid.NewGuid().ToString(),
                RecipeTitle = recipe.RecipeTitle,
                RecipeImage = recipe.RecipeImage,
                RecipeInredients = recipe.RecipeInredients,
                RecipeDescription = recipe.RecipeDescription,
                RecipeIntroduction = recipe.RecipeIntroduction,
                RecipeDirections = recipe.RecipeDirections,
                RecipeCookTime = recipe.RecipeCookTime,
                RecipeCalories = recipe.RecipeCalories,
                RecipeServings = recipe.RecipeServings,
                RecipeAuthorId = author.Id,
                RecipeCategoryId = category.RecipeCategoryId,
                CuisineId = cuisine.CuisineId
            };
            context.Add(recipeDb);
            await context.SaveChangesAsync();
        }

        //deleteRecipe
        public async Task DeleteRecipe(string id)
        {
            if (string.IsNullOrEmpty(id) || string.IsNullOrWhiteSpace(id))
            {
                Console.WriteLine("Eror!");
            }
            if (id != null)
            {
                var recipeDb = context.Recipe.FirstOrDefault(x => x.RecipeId == id);
                context.Recipe.Remove(recipeDb);
                await context.SaveChangesAsync();
            }
        }

        //updateById
        public RecipeViewModel UpdateById(string recipeId)
        {
            RecipeViewModel recipe = context.Recipe
                .Select(recipe => new RecipeViewModel
                {
                    RecipeId = recipe.RecipeId,
                    RecipeTitle = recipe.RecipeTitle,
                    RecipeImage = recipe.RecipeImage,
                    RecipeInredients = recipe.RecipeInredients,
                    RecipeDescription = recipe.RecipeDescription,
                    User = recipe.User,
                    RecipeIntroduction = recipe.RecipeIntroduction,
                    RecipeDirections = recipe.RecipeDirections,
                    RecipeCookTime = recipe.RecipeCookTime,
                    RecipeCalories = recipe.RecipeCalories,
                    RecipeServings = recipe.RecipeServings,
                }).SingleOrDefault(recipe => recipe.RecipeId == recipeId);

            return recipe;
        }

        //updateAsync
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
            recipe.RecipeImage = model.RecipeImage;
            recipe.RecipeInredients = model.RecipeInredients;
            recipe.RecipeDescription = model.RecipeDescription;
            recipe.User = model.User;
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
