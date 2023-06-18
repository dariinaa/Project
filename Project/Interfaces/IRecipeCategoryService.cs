using Project.Services.ViewModels;

namespace Project.Interfaces
{
    public interface IRecipeCategoryService
    {
        List<RecipeCategoryViewModel> GetAll();
        RecipeCategoryViewModel GetDetailsById(string recipeCategoryId);
        Task AddRecipeCategory(RecipeCategoryViewModel category);
        Task DeleteRecipeCategory(string id);
        Task UpdateRecipeCategory(RecipeCategoryViewModel recipeCategory);
        List<RecipeViewModel> GetRecipesByCategory(string categoryId);
        IEnumerable<string> GetAllRecipeCategoryNames();
    }
}
