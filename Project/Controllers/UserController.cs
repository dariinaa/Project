using Microsoft.AspNetCore.Mvc;
using Project.Data.DataModels;
using Project.Services;
using Project.Services.ViewModels;

namespace Project.Controllers
{
    public class UserController:Controller
    {
        public UserService userService { get; set; }
        public UserController(UserService service)
        {
            userService = service;
        }
        [HttpGet]
        public IActionResult Index()
        {
            List<User> users = userService.GetAll();

            return this.View(users);
        }
    }
}
