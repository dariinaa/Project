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

        //get all recipe
        public List<RecipeViewModel> GetAll()
        {
            return context.Recipe.Select(recipe => new RecipeViewModel()
            {
                RecipeId = recipe.RecipeId,
                RecipeTitle = recipe.RecipeTitle,
                RecipeImage = recipe.RecipeImage,
                RecipeDescription = recipe.RecipeDescription,
                RecipeCookTime = recipe.RecipeCookTime,
            }).ToList();
        }

        //get details recipe
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
                    RecipeIntroduction = recipe.RecipeIntroduction,
                    RecipeDirections = recipe.RecipeDirections,
                    RecipeCookTime = recipe.RecipeCookTime,
                    RecipeCalories = recipe.RecipeCalories,
                    RecipeServings = recipe.RecipeServings,
                    RecipeAuthorId = recipe.RecipeAuthorId,
                    User = recipe.User,
                    RecipeCategoryId = recipe.RecipeCategoryId,
                    RecipeCategory = recipe.RecipeCategory,
                    CuisineId = recipe.CuisineId,
                    Cuisine = recipe.Cuisine,
                    ReviewId = recipe.ReviewId,
                    RecipeReviews = recipe.RecipeReviews,
                }).SingleOrDefault(recipe => recipe.RecipeId == recipeId);
            return recipe;
        }

        //add recipe
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
            if (category.RecipeCategoryRecipes == null)
            {
                category.RecipeCategoryRecipes = new List<Recipe>();
            }
            category.RecipeCategoryRecipes.Add(recipeDb);
            context.Add(recipeDb);
            await context.SaveChangesAsync();

            if (cuisine.CuisineRecipes == null)
            {
                cuisine.CuisineRecipes = new List<Recipe>();
            }
            cuisine.CuisineRecipes.Add(recipeDb);
            context.Add(recipeDb);
            await context.SaveChangesAsync();
        }

        //delete recipe
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

        //update by Id
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
                    RecipeIntroduction = recipe.RecipeIntroduction,
                    RecipeDirections = recipe.RecipeDirections,
                    RecipeCookTime = recipe.RecipeCookTime,
                    RecipeCalories = recipe.RecipeCalories,
                    RecipeServings = recipe.RecipeServings,
                    RecipeAuthorId = recipe.RecipeAuthorId,
                    User = recipe.User,
                    RecipeCategoryId = recipe.RecipeCategoryId,
                    RecipeCategory = recipe.RecipeCategory,
                    CuisineId = recipe.CuisineId,
                    Cuisine = recipe.Cuisine,
                    ReviewId = recipe.ReviewId,
                    RecipeReviews = recipe.RecipeReviews,
                }).SingleOrDefault(recipe => recipe.RecipeId == recipeId);
            return recipe;
        }

        //update recipe
        public async Task UpdateRecipe(RecipeViewModel recipe)
        {
            var recipeDb = await context.Recipe.FirstOrDefaultAsync(r => r.RecipeId == recipe.RecipeId);

            if (recipeDb == null)
            {
                throw new Exception("Recipe not found.");
            }
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
            //recipeDb.RecipeAuthorId = recipe.RecipeAuthorId;
            //recipeDb.RecipeCategoryId = recipe.RecipeCategoryId;
            //recipeDb.CuisineId = recipe.CuisineId;
            context.Recipe.Update(recipeDb);
            await context.SaveChangesAsync();
        }

        //get reviews of a specific recipe
        public List<ReviewViewModel> GetRecipeReviews(string recipeId)
        {
            var recipe = context.Recipe
                .Include(r => r.RecipeReviews)
                .FirstOrDefault(r => r.RecipeId == recipeId);

            if (recipe == null)
            {
                throw new Exception("Recipe not found.");
            }

            var reviews = recipe.RecipeReviews.Select(recipe => new ReviewViewModel
            {
                ReviewId = recipe.ReviewId,
                ReviewMessage = recipe.ReviewMessage,
                ReviewDate = recipe.ReviewDate,
                RecipeId = recipe.RecipeId,
                ReviewAuthorId = context.Users.FirstOrDefault(u => u.Id == recipe.ReviewAuthorId).UserName,
            }).ToList();
            return reviews;
        }

        /*public RecipeViewModel GetDetailsById(string recipeId)
        {
            RecipeViewModel recipe = context.Recipe
                .Where(r => r.RecipeId == recipeId)
                .Join(context.User, recipe => recipe.RecipeAuthorId, user => user.Id, (recipe, user) => new { recipe, user })
                .Join(context.RecipeCategory, ru => ru.recipe.RecipeCategoryId, category => category.RecipeCategoryId, (ru, category) => new { ru.recipe, ru.user, category })
                .Join(context.Cuisine, ruc => ruc.recipe.CuisineId, cuisine => cuisine.CuisineId, (ruc, cuisine) => new { ruc.recipe, ruc.user, ruc.category, cuisine })
                .Select(result => new RecipeViewModel
                {
                    RecipeId = result.recipe.RecipeId,
                    RecipeTitle = result.recipe.RecipeTitle,
                    RecipeImage = result.recipe.RecipeImage,
                    RecipeInredients = result.recipe.RecipeInredients,
                    RecipeDescription = result.recipe.RecipeDescription,
                    RecipeIntroduction = result.recipe.RecipeIntroduction,
                    RecipeDirections = result.recipe.RecipeDirections,
                    RecipeCookTime = result.recipe.RecipeCookTime,
                    RecipeCalories = result.recipe.RecipeCalories,
                    RecipeServings = result.recipe.RecipeServings,
                    RecipeAuthorId = result.user.UserName, // Changed property
                    RecipeCategoryId = result.category.RecipeCategoryName, // Changed property
                    CuisineId = result.cuisine.CuisineName, // Changed property
                    ReviewId = result.recipe.ReviewId,
                    RecipeReviews = result.recipe.RecipeReviews
                })
                .SingleOrDefault();

            return recipe;
        }*/
    }
}
