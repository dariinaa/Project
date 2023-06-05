using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Data.DataModels;
using Project.Services.ViewModels;

namespace Project.Services
{
    public class CuisineService
    {
        private readonly ApplicationDbContext context;
        public CuisineService(ApplicationDbContext post)
        {
            context = post;
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
            CuisineViewModel cuisine = context.Cuisine
                .Select(cuisine => new CuisineViewModel
                {
                    CuisineId = cuisine.CuisineId,
                    CuisineName = cuisine.CuisineName,
                    CuisineImage = cuisine.CuisineImage,
                }).SingleOrDefault(cuisine => cuisine.CuisineId == cuisineId);
            return cuisine;
        }

        //add cuisine
        public async Task AddCuisine(CuisineViewModel cuisine)
        {
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
                Console.WriteLine("Eror!");
            }
            if (id != null)
            {
                var cuisineDb = context.Cuisine.FirstOrDefault(x => x.CuisineId == id);
                context.Cuisine.Remove(cuisineDb);
                await context.SaveChangesAsync();
            }
        }

        //update cuisine
        public async Task UpdateCuisine(CuisineViewModel cuisine)
        {
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
            var cuisine = context.Cuisine
                .Include(c => c.CuisineRecipes)
                .FirstOrDefault(c => c.CuisineId == cuisineId);
            if (cuisine == null)
            {
                throw new Exception("Category not found.");
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
            }).ToList();
            return recipes;
        }
    }
}