using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Data.DataModels;
using Project.Interfaces;
using Project.Services.ViewModels;

namespace Project.Services
{
    public class RecipeCategoryService: IRecipeCategoryService
    {
        private readonly ApplicationDbContext context;
        public RecipeCategoryService(ApplicationDbContext post)
        {
            context = post;
        }

        //get all category
        public List<RecipeCategoryViewModel> GetAll()
        {
            return context.RecipeCategory.Select(recipeCategory => new RecipeCategoryViewModel()
            {
                RecipeCategoryId = recipeCategory.RecipeCategoryId,
                RecipeCategoryName = recipeCategory.RecipeCategoryName,
                RecipeCategoryImage = recipeCategory.RecipeCategoryImage,
            }).ToList();
        }

        //get details recipe category
        public RecipeCategoryViewModel GetDetailsById(string recipeCategoryId)
        {
            RecipeCategoryViewModel category = context.RecipeCategory
                .Select(category => new RecipeCategoryViewModel
                {
                    RecipeCategoryId = category.RecipeCategoryId,
                    RecipeCategoryName = category.RecipeCategoryName,
                    RecipeCategoryImage = category.RecipeCategoryImage,
                }).SingleOrDefault(category => category.RecipeCategoryId == recipeCategoryId);
            return category;
        }

        //add recipe category
        public async Task AddRecipeCategory(RecipeCategoryViewModel category)
        {
            var categoryDb = new RecipeCategory
            {
                RecipeCategoryId = Guid.NewGuid().ToString(),
                RecipeCategoryName = category.RecipeCategoryName,
                RecipeCategoryImage = category.RecipeCategoryImage,               
            };
            context.Add(categoryDb);
            await context.SaveChangesAsync();
        }

        //delete recipe category
        public async Task DeleteRecipeCategory(string id)
        {
            if (string.IsNullOrEmpty(id) || string.IsNullOrWhiteSpace(id))
            {
                Console.WriteLine("Eror!");
            }
            if (id != null)
            {
                var recipeCategoryDb = context.RecipeCategory.FirstOrDefault(x => x.RecipeCategoryId == id);
                context.RecipeCategory.Remove(recipeCategoryDb);
                await context.SaveChangesAsync();
            }
        }

        //update recipe category
        public async Task UpdateRecipeCategory(RecipeCategoryViewModel recipeCategory)
        {
            var recipeCategoryDb = await context.RecipeCategory.FirstOrDefaultAsync(r => r.RecipeCategoryId == recipeCategory.RecipeCategoryId);

            if (recipeCategoryDb == null)
            {
                throw new Exception("Recipe Category not found.");
            }
            recipeCategoryDb.RecipeCategoryId = recipeCategory.RecipeCategoryId;
            recipeCategoryDb.RecipeCategoryName = recipeCategory.RecipeCategoryName;
            recipeCategoryDb.RecipeCategoryImage = recipeCategory.RecipeCategoryImage;        
            context.RecipeCategory.Update(recipeCategoryDb);
            await context.SaveChangesAsync();
        }

        //get recipes by category
        public List<RecipeViewModel> GetRecipesByCategory(string categoryId)
        {
            var category = context.RecipeCategory
                .Include(c => c.RecipeCategoryRecipes)
                .FirstOrDefault(c => c.RecipeCategoryId == categoryId);
            if (category == null)
            {
                throw new Exception("Category not found.");
            }

            var recipes = category.RecipeCategoryRecipes.Select(recipe => new RecipeViewModel
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
