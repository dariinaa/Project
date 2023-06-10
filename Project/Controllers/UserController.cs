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

        private string GetFullErrorMessage(Exception ex)
        {
            string errorMessage = ex.Message;

            if (ex.InnerException != null)
            {
                errorMessage += " Inner Exception: " + ex.InnerException.Message;
            }

            return errorMessage;
        }

        //index
        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                List<UserViewModel> users = userService.GetAll();

                return this.View(users);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"An error occurred while retrieving users: {GetFullErrorMessage(ex)}");
                return View();
            }
        }

        //delete confirmed
        [HttpGet]
        public IActionResult DeleteConfirmed()
        {
            return this.View();
        }

        //delete user
        [HttpGet]
        public IActionResult Delete(string id)
        {
            try
            {
                _ = userService.DeleteUser(id);
                return RedirectToAction("DeleteConfirmed");
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError("", $"An error occurred while deleting the user: {GetFullErrorMessage(ex)}");
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
