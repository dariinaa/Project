using Project.Services.ViewModels;

namespace Project.Interfaces
{
    public interface ICuisineService
    {
        List<CuisineViewModel> GetAll();
        CuisineViewModel GetDetailsById(string cuisineId);
        Task AddCuisine(CuisineViewModel cuisine);
        Task DeleteCuisine(string id);
        Task UpdateCuisine(CuisineViewModel cuisine);
        List<RecipeViewModel> GetRecipesByCuisine(string cuisineId);
    }
}
