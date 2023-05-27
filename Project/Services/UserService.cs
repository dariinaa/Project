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

        //getAll
        public List<User> GetAll()
        {
            return context.User.Select(user => new User()
            {
                UserName = user.UserName,
                Email = user.Email,
                UserCity = user.UserCity,
            }).ToList();
        }
    }
}
