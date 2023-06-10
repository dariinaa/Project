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
                throw new Exception("User ID is invalid.");
            }

            var userDb = context.Users.FirstOrDefault(x => x.Id == id);

            if (userDb == null)
            {
                throw new ArgumentNullException(nameof(userDb), "User not found.");
            }

            context.Users.Remove(userDb);
            await context.SaveChangesAsync();
        }
    }
}
