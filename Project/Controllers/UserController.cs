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

        //index
        [HttpGet]
        public IActionResult Index()
        {
            List<UserViewModel> users = userService.GetAll();

            return this.View(users);
        }

        //delete user
        [HttpGet]
        public IActionResult Delete(string id)
        {
            try
            {
                _ = userService.DeleteUser(id);
                return RedirectToAction("Index");
            }
            catch (ArgumentException e)
            {
                ViewData["Message"] = e.Message;

                return RedirectToAction(nameof(Index));
            }
        }

        //get recipes by user
        /*public IActionResult GetRecipesByUser(string id)
        {
            List<RecipeViewModel> recipeUserRecipes = userService.GetRecipesByUser(id);
            return this.View(recipeUserRecipes);
        }*/
    }
}
