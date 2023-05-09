using Project.Data;
using Project.Data.DataModels;
using Project.Services.ViewModels;

namespace Project.Services
{
    public class UserService
    {
        private readonly ApplicationDbContext context;
        public UserService(ApplicationDbContext post)
        {
            context = post;
        }
        public List<User> GetAll()
        {
            return context.User.Select(recipe => new User()
            {
                UserName = recipe.UserName,
            }).ToList();
        }
        public User GetDetailsById(string userId)
        {
            User user = context.User
                .Select(recipe => new User
                {
                    UserName = recipe.UserName,
                    Email = recipe.Email,
                    UserCity = recipe.UserCity,
                }).SingleOrDefault(user => user.Id == userId);

            return user;
        }
        public async Task AddUser(User user)
        {
            var userDb = new User();
            userDb.UserName = user.UserName;
            userDb.Email = user.Email;
            userDb.PasswordHash = user.PasswordHash;
            userDb.PhoneNumber = user.PhoneNumber;
            userDb.UserCity = user.UserCity;
            if (user.UserName != null)
            {
                context.Add(userDb);
                await context.SaveChangesAsync();
            }
            else
            {
                Console.WriteLine("Eror!");
            }
        }
    }
}
