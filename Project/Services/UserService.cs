using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<IdentityUser> _userManager;

        public UserService(ApplicationDbContext post, UserManager<IdentityUser> userManager)
        {
            context = post;
            _userManager = userManager;
        }

        //get all user
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

            var reviews = context.Review.Where(r => r.ReviewAuthorId == id);
            context.Review.RemoveRange(reviews);

            var recipes = context.Recipe.Where(r => r.RecipeAuthorId == id);
            context.Recipe.RemoveRange(recipes);

            var userRoleMappings = context.UserRoles.Where(ur => ur.UserId == id);
            context.UserRoles.RemoveRange(userRoleMappings);

            context.Users.Remove(userDb);
            await context.SaveChangesAsync();
        }

        //change role user
        public void ChangeUserRole(string userId, string newRole)
        {
            var user = _userManager.FindByIdAsync(userId).Result;
            if (user != null)
            {
                var userRoles = _userManager.GetRolesAsync(user).Result;

                _userManager.RemoveFromRolesAsync(user, userRoles).Wait();

                _userManager.AddToRoleAsync(user, newRole).Wait();
            }
        }

        // get user role
        public string GetUserRole(string userId)
        {
            var user = context.Users.FirstOrDefault(u => u.Id == userId);

            if (user != null)
            {
                try
                {
                    var roles = _userManager.GetRolesAsync(user).Result;

                    if (roles.Any())
                    {
                        return roles.First();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred while retrieving roles for user {user.UserName}: {ex.Message}");
                }
            }
            return string.Empty;
        }
    }
}
