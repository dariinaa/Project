using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Data.DataModels;
using Project.Interfaces;
using Project.Services.ViewModels;

namespace Project.Services
{
    public class CuisineService: ICuisineService
    {
        private readonly ApplicationDbContext context;
        private readonly IRecipeService recipeService;

        public CuisineService(ApplicationDbContext post, IRecipeService _recipeService)
        {
            context = post;
            recipeService = _recipeService;
        }

        //get all cuisine
        public List<CuisineViewModel> GetAll()
        {
            return context.Cuisine.Select(cuisine => new CuisineViewModel()
            {
                CuisineId = cuisine.CuisineId,
                CuisineName = cuisine.CuisineName,
                CuisineImage = cuisine.CuisineImage,
            }).ToList();
        }

        //get details cuisine
        public CuisineViewModel GetDetailsById(string cuisineId)
        {
            if (string.IsNullOrEmpty(cuisineId) || string.IsNullOrWhiteSpace(cuisineId))
            {
                throw new ArgumentException("Cuisine ID is invalid.", nameof(cuisineId));
            }

            CuisineViewModel cuisine = context.Cuisine
                .Select(cuisine => new CuisineViewModel
                    {
                        CuisineId = cuisine.CuisineId,
                        CuisineName = cuisine.CuisineName,
                        CuisineImage = cuisine.CuisineImage,
                    }).SingleOrDefault(cuisine => cuisine.CuisineId == cuisineId);

            if (cuisine == null)
            {
                throw new ArgumentNullException(nameof(cuisine), "Cuisine not found.");
            }

            return cuisine;
        }

        //add cuisine
        public async Task AddCuisine(CuisineViewModel cuisine)
        {
            if (cuisine == null)
            {
                throw new ArgumentNullException(nameof(cuisine), "Cuisine is invalid.");
            }

            var cuisineDb = new Cuisine
            {
                CuisineId = Guid.NewGuid().ToString(),
                CuisineName = cuisine.CuisineName,
                CuisineImage = cuisine.CuisineImage,
            };

            context.Add(cuisineDb);
            await context.SaveChangesAsync();
        }

        //delete cuisine
        public async Task DeleteCuisine(string id)
        {
            if (string.IsNullOrEmpty(id) || string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException("Cuisine ID is invalid.", nameof(id));
            }

            var cuisineDb = context.Cuisine.FirstOrDefault(x => x.CuisineId == id);

            if (cuisineDb == null)
            {
                throw new ArgumentNullException(nameof(cuisineDb), "Cuisine not found.");
            }

            var recipesToDelete = context.Recipe.Where(r => r.CuisineId == id).ToList();

            foreach (var recipe in recipesToDelete)
            {
                var reviews = context.Review.Where(r => r.RecipeId == recipe.RecipeId).ToList();
                context.Review.RemoveRange(reviews);
            }

            context.Recipe.RemoveRange(recipesToDelete);
            context.Cuisine.Remove(cuisineDb);

            await context.SaveChangesAsync();
        }

        //update cuisine
        public async Task UpdateCuisine(CuisineViewModel cuisine)
        {
            if (cuisine == null)
            {
                throw new ArgumentNullException(nameof(cuisine), "Cuisine is invalid.");
            }

            var cuisineDb = await context.Cuisine.FirstOrDefaultAsync(r => r.CuisineId == cuisine.CuisineId);

            if (cuisineDb == null)
            {
                throw new Exception("Cuisine not found.");
            }

            cuisineDb.CuisineId = cuisine.CuisineId;
            cuisineDb.CuisineName = cuisine.CuisineName;
            cuisineDb.CuisineImage = cuisine.CuisineImage;
            context.Cuisine.Update(cuisineDb);
            await context.SaveChangesAsync();
        }

        //get recipes by cuisine
        public List<RecipeViewModel> GetRecipesByCuisine(string cuisineId)
        {
            if (string.IsNullOrEmpty(cuisineId) || string.IsNullOrWhiteSpace(cuisineId))
            {
                throw new ArgumentException("Cuisine ID is invalid.", nameof(cuisineId));
            }

            var cuisine = context.Cuisine
                .Include(c => c.CuisineRecipes)
                .FirstOrDefault(c => c.CuisineId == cuisineId);

            if (cuisine == null)
            {
                throw new Exception("Cuisine not found.");
            }

            var recipes = cuisine.CuisineRecipes.Select(recipe => new RecipeViewModel
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
                RecipeAuthorName = context.Users.FirstOrDefault(x => x.Id == recipe.RecipeAuthorId).UserName,
            }).ToList();

            if (recipes == null)
            {
                throw new Exception("Recipes not found.");
            }

            return recipes;
        }

        //get all cuisine names
        public IEnumerable<string> GetAllCuisineNames()
        {
            return context.Cuisine.Select(c => c.CuisineName);
        }

    }
}