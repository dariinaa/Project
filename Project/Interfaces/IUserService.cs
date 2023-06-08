using Project.Services.ViewModels;

namespace Project.Interfaces
{
    public interface IUserService
    {
        List<UserViewModel> GetAll();
        Task DeleteUser(string id);
        // List<RecipeViewModel> GetRecipesByUser(string userId);
    }
}
