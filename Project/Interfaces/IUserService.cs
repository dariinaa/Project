using Project.Services.ViewModels;

namespace Project.Interfaces
{
    public interface IUserService
    {
        List<UserViewModel> GetAll();
        Task DeleteUser(string id);
        void ChangeUserRole(string userId, string newRole);
        string GetUserRole(string userId);
    }
}
