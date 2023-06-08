using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Data.DataModels;
using Project.Interfaces;
using Project.Services.ViewModels;

namespace Project.Services
{
    public class UserService: IUserService
    {
        private readonly ApplicationDbContext context;
        public UserService(ApplicationDbContext post)
        {
            context = post;
        }

        //getAll
        public List<UserViewModel> GetAll()
        {
            return context.Users.Select(user => new UserViewModel()
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
            }).ToList();
        }

        //delete user
        public async Task DeleteUser(string id)
        {
            if (string.IsNullOrEmpty(id) || string.IsNullOrWhiteSpace(id))
            {
                Console.WriteLine("Eror!");
            }
            if (id != null)
            {
                var userDb = context.Users.FirstOrDefault(x => x.Id == id);
                context.Users.Remove(userDb);
                await context.SaveChangesAsync();
            }
        }

        //get recipes by user
        /*public List<RecipeViewModel> GetRecipesByUser(string userId)
        {
            var user = context.Users
                .Include(c => c.UserRecipes)
                .FirstOrDefault(c => c.Id == userId);
            if (user == null)
            {
                throw new Exception("User not found.");
            }

            var recipes = user.UserRecipes.Select(recipe => new RecipeViewModel
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
        }*/
    }
}
