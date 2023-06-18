using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Data.DataModels;
using Project.Interfaces;
using Project.Services.ViewModels;
using System.Security.Claims;

namespace Project.Services
{
    public class RecipeService: IRecipeService
    {
        private readonly ApplicationDbContext context;
        
        private readonly UserManager<IdentityUser> _userManager;

        public RecipeService(ApplicationDbContext post, UserManager<IdentityUser> userManager)
        {
            context = post;
            _userManager = userManager;
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
                RecipeAuthorName = context.Users.FirstOrDefault(x => x.Id == recipe.RecipeAuthorId).UserName,
            }).ToList();
        }

        //get details recipe
        public RecipeViewModel GetDetailsById(string recipeId)
        {
            if (string.IsNullOrEmpty(recipeId))
            {
                throw new ArgumentException("Invalid recipe ID.");
            }

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

                    RecipeAuthorName = context.Users.FirstOrDefault(u => u.Id == recipe.RecipeAuthorId).UserName,
                    RecipeAuthorId = context.Users.FirstOrDefault(u => u.Id == recipe.RecipeAuthorId).Id,
                    User = recipe.User,

                    RecipeCategoryId = recipe.RecipeCategoryId,
                    RecipeCategory = context.RecipeCategory.FirstOrDefault(rc => rc.RecipeCategoryId == recipe.RecipeCategoryId),
                    
                    CuisineId = recipe.CuisineId,
                    Cuisine = context.Cuisine.FirstOrDefault(c => c.CuisineId == recipe.CuisineId),
                    
                    ReviewId = recipe.ReviewId,
                    RecipeReviews = recipe.RecipeReviews,
                }).SingleOrDefault(recipe => recipe.RecipeId == recipeId);

            if (recipe == null)
            {
                throw new ArgumentNullException(nameof(recipe), "Recipe not found.");
            }

            return recipe;
        }

        //add recipe
        public async Task AddRecipe(RecipeViewModel recipe, ClaimsPrincipal user)
        {
            if (recipe == null)
            {
                throw new ArgumentNullException(nameof(recipe), "Recipe is invalid.");
            }

            var userId = _userManager.GetUserId(user);

            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentException("Invalid user ID.");
            }

            var category = await context.RecipeCategory.FirstOrDefaultAsync(rc => rc.RecipeCategoryName == recipe.RecipeCategoryName);
            if (category == null)
            {
                throw new ArgumentException("Invalid recipe category.");
            }

            var cuisine = await context.Cuisine.FirstOrDefaultAsync(c => c.CuisineName == recipe.CuisineName);
            if (cuisine == null)
            {
                throw new ArgumentException("Invalid cuisine.");
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
                RecipeAuthorId = userId,
                RecipeCategoryId = category.RecipeCategoryId,
                CuisineId = cuisine.CuisineId
            };

            category.RecipeCategoryRecipes ??= new List<Recipe>();
            category.RecipeCategoryRecipes.Add(recipeDb);

            cuisine.CuisineRecipes ??= new List<Recipe>();
            cuisine.CuisineRecipes.Add(recipeDb);

            context.Add(recipeDb);
            await context.SaveChangesAsync();
        }

        //delete recipe
        public async Task DeleteRecipe(string id)
        {
            if (string.IsNullOrEmpty(id) || string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException("Recipe ID is invalid.", nameof(id));
            }

            if (id != null)
            {
                var recipeDb = context.Recipe.FirstOrDefault(x => x.RecipeId == id);

                if (recipeDb == null)
                {
                    throw new ArgumentNullException(nameof(recipeDb), "Recipe not found.");
                }

                context.Recipe.Remove(recipeDb);
                await context.SaveChangesAsync();
            }
        }

        //update by Id
        public RecipeViewModel UpdateById(string recipeId)
        {
            if (string.IsNullOrEmpty(recipeId))
            {
                throw new ArgumentException("Invalid recipe ID.");
            }

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

                    RecipeAuthorName = context.Users.FirstOrDefault(u => u.Id == recipe.RecipeAuthorId).UserName,
                    RecipeAuthorId = context.Users.FirstOrDefault(u => u.Id == recipe.RecipeAuthorId).Id,
                    User = recipe.User,

                    RecipeCategoryName = context.RecipeCategory.FirstOrDefault(rc => rc.RecipeCategoryId == recipe.RecipeCategoryId).RecipeCategoryName,
                    RecipeCategoryId = recipe.RecipeCategoryId,
                    RecipeCategory = context.RecipeCategory.FirstOrDefault(rc => rc.RecipeCategoryId == recipe.RecipeCategoryId),

                    CuisineName = context.Cuisine.FirstOrDefault(c => c.CuisineId == recipe.CuisineId).CuisineName,
                    CuisineId = recipe.CuisineId,
                    Cuisine = context.Cuisine.FirstOrDefault(c => c.CuisineId == recipe.CuisineId),
                    
                    ReviewId = recipe.ReviewId,
                    RecipeReviews = recipe.RecipeReviews,
                }).SingleOrDefault(recipe => recipe.RecipeId == recipeId);

            if (recipe == null)
            {
                throw new ArgumentNullException(nameof(recipe), "Recipe not found.");
            }

            return recipe;
        }

        //update recipe
        public async Task UpdateRecipe(RecipeViewModel recipe)
        {
            if (recipe == null)
            {
                throw new ArgumentNullException(nameof(recipe), "Recipe is invalid.");
            }

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

            recipeDb.RecipeCategory = await context.RecipeCategory.FirstOrDefaultAsync(x => x.RecipeCategoryName == recipe.RecipeCategoryName);
            recipeDb.Cuisine = await context.Cuisine.FirstOrDefaultAsync(x => x.CuisineName == recipe.CuisineName);

            //recipeDb.RecipeCategoryId = context.RecipeCategory.Where(x=>x.RecipeCategoryName == recipe.RecipeCategoryName).FirstOrDefault().RecipeCategoryId;
            
            //recipeDb.CuisineId = context.Cuisine.Where(x => x.CuisineName == recipe.CuisineName).FirstOrDefault().CuisineId;
            
            context.Recipe.Update(recipeDb);
            await context.SaveChangesAsync();
        }

        //get reviews of a specific recipe
        public List<ReviewViewModel> GetRecipeReviews(string recipeId)
        {
            if (string.IsNullOrEmpty(recipeId) || string.IsNullOrWhiteSpace(recipeId))
            {
                throw new ArgumentException("Recipe ID is invalid.", nameof(recipeId));
            }

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
                //ReviewAuthorId = context.Users.FirstOrDefault(u => u.Id == recipe.ReviewAuthorId).UserName,
                ReviewAuthorName = context.Users.FirstOrDefault(u => u.Id == recipe.ReviewAuthorId).UserName,
            }).ToList();

            if (reviews == null)
            {
                throw new Exception("Reviews not found.");
            }

            return reviews;
        }
    }
}
