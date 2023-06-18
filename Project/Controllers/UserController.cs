using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project.Data.DataModels;
using Project.Interfaces;
using Project.Services;
using Project.Services.ViewModels;

namespace Project.Controllers
{
    public class UserController:Controller
    {
        public IUserService userService { get; set; }
        private readonly UserManager<IdentityUser> _userManager;

        public UserController(IUserService service, UserManager<IdentityUser> userManager)
        {
            userService = service;
            _userManager = userManager;
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

        //make admin
        [HttpPost]
        public IActionResult MakeAdmin(string userId)
        {
            if (!User.IsInRole("Admin"))
            {
                return Forbid();
            }
            var user = _userManager.FindByIdAsync(userId).Result;
            if (user != null)
            {
                userService.ChangeUserRole(userId, "Admin");
                return RedirectToAction("Index");
            }
            return NotFound();
        }

        //make user
        [HttpPost]
        public IActionResult MakeUser(string userId)
        {
            if (!User.IsInRole("Admin"))
            {
                return Forbid();
            }
            var user = _userManager.FindByIdAsync(userId).Result;
            if (user != null)
            {
                userService.ChangeUserRole(userId, "User");
                return RedirectToAction("Index");
            }
            return NotFound();
        }

        //make chef
        [HttpPost]
        public IActionResult MakeChef(string userId)
        {
            if (!User.IsInRole("Admin"))
            {
                return Forbid();
            }
            var user = _userManager.FindByIdAsync(userId).Result;
            if (user != null)
            {
                userService.ChangeUserRole(userId, "Chef");
                return RedirectToAction("Index");
            }
            return NotFound();
        }
    }
}
