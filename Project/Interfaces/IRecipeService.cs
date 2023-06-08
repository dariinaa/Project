using Project.Services.ViewModels;
using System.Security.Claims;

namespace Project.Interfaces
{
    public interface IRecipeService
    {
        List<RecipeViewModel> GetAll();
        RecipeViewModel GetDetailsById(string recipeId);
        Task AddRecipe(RecipeViewModel recipe, ClaimsPrincipal user);
        Task DeleteRecipe(string id);
        RecipeViewModel UpdateById(string recipeId);
        Task UpdateRecipe(RecipeViewModel recipe);
        List<ReviewViewModel> GetRecipeReviews(string recipeId);
    }
}
