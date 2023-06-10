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

        //get all recipe category
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
            if (string.IsNullOrEmpty(recipeCategoryId))
            {
                throw new ArgumentException("Invalid recipe category ID.");
            }

            RecipeCategoryViewModel category = context.RecipeCategory
                .Select(category => new RecipeCategoryViewModel
                {
                    RecipeCategoryId = category.RecipeCategoryId,
                    RecipeCategoryName = category.RecipeCategoryName,
                    RecipeCategoryImage = category.RecipeCategoryImage,
                }).SingleOrDefault(category => category.RecipeCategoryId == recipeCategoryId);

            if (category == null)
            {
                throw new ArgumentNullException(nameof(category), "Recipe category not found.");
            }

            return category;
        }

        //add recipe category
        public async Task AddRecipeCategory(RecipeCategoryViewModel category)
        {
            if (category == null)
            {
                throw new ArgumentNullException(nameof(category), "Recipe category is invalid.");
            }

            var categoryDb = new RecipeCategory
            {
                RecipeCategoryId = Guid.NewGuid().ToString(),
                RecipeCategoryName = category.RecipeCategoryName,
                RecipeCategoryImage = category.RecipeCategoryImage,
            };

            context.Add(categoryDb);
            await context.SaveChangesAsync(); ;
        }

        //delete recipe category
        public async Task DeleteRecipeCategory(string id)
        {
            if (string.IsNullOrEmpty(id) || string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException("Recipe category ID is invalid.", nameof(id));
            }

            var recipeCategoryDb = context.RecipeCategory.FirstOrDefault(x => x.RecipeCategoryId == id);

            if (recipeCategoryDb == null)
            {
                throw new ArgumentNullException(nameof(recipeCategoryDb), "Recipe category not found.");
            }

            context.RecipeCategory.Remove(recipeCategoryDb);
            await context.SaveChangesAsync();
        }

        //update recipe category
        public async Task UpdateRecipeCategory(RecipeCategoryViewModel recipeCategory)
        {
            if (recipeCategory == null)
            {
                throw new ArgumentNullException(nameof(recipeCategory), "Recipe category is invalid.");
            }

            var recipeCategoryDb = await context.RecipeCategory.FirstOrDefaultAsync(r => r.RecipeCategoryId == recipeCategory.RecipeCategoryId);

            if (recipeCategoryDb == null)
            {
                throw new ArgumentNullException(nameof(recipeCategoryDb), "Recipe category not found.");
            }

            recipeCategoryDb.RecipeCategoryName = recipeCategory.RecipeCategoryName;
            recipeCategoryDb.RecipeCategoryImage = recipeCategory.RecipeCategoryImage;

            context.RecipeCategory.Update(recipeCategoryDb);
            await context.SaveChangesAsync();
        }

        //get recipes by category
        public List<RecipeViewModel> GetRecipesByCategory(string categoryId)
        {
            if (string.IsNullOrEmpty(categoryId) || string.IsNullOrWhiteSpace(categoryId))
            {
                throw new ArgumentException("Recipe category ID is invalid.", nameof(categoryId));
            }

            var category = context.RecipeCategory
                .Include(c => c.RecipeCategoryRecipes)
                .FirstOrDefault(c => c.RecipeCategoryId == categoryId);

            if (category == null)
            {
                throw new ArgumentNullException(nameof(category), "Recipe category not found.");
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

            if (recipes == null)
            {
                throw new Exception("Recipes not found.");
            }

            return recipes;
        }

        //get all recipe category names
        public IEnumerable<string> GetAllRecipeCategoryNames()
        {
            return context.RecipeCategory.Select(c => c.RecipeCategoryName);
        }
    }
}
