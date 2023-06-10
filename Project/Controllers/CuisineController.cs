using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Services;
using Project.Services.ViewModels;

namespace Project.Controllers
{
    public class CuisineController : Controller
    {

        public CuisineService cuisineService { get; set; }

        public CuisineController(CuisineService service)
        {
            cuisineService = service;
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
        [Authorize]
        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                var cuisines = cuisineService.GetAll();
                return View(cuisines);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred while retrieving cuisines: {GetFullErrorMessage(ex)}");
                return View();
            }
        }

        //deletion complete
        public IActionResult DeletionComplete()
        {
            return this.View();
        }

        //cuisine category
        [HttpGet]
        public IActionResult Details(string id)
        {
            try
            {
                CuisineViewModel cuisine = cuisineService.GetDetailsById(id);
                return View(cuisine);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred while retrieving cuisine details: {GetFullErrorMessage(ex)}");
                return RedirectToAction(nameof(Index));
            }
        }

        //add cuisine
        [HttpGet]
        public IActionResult AddCuisine()
        {
            return View();
        }

        //add cuisine
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCuisine([Bind("CuisineId", "CuisineName", "CuisineImage")] CuisineViewModel cuisineVM)
        {
            try
            {
                await cuisineService.AddCuisine(cuisineVM);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred while adding cuisine: {GetFullErrorMessage(ex)}");
                return View(cuisineVM);
            }
        }

        //update cuisine
        [HttpGet]
        public IActionResult Update(string id)
        {
            try
            {
                CuisineViewModel cuisine = cuisineService.GetDetailsById(id);
                return View(cuisine);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred while retrieving cuisine details: {GetFullErrorMessage(ex)}");
                return RedirectToAction(nameof(Index));
            }
        }

        //update cuisine
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Update(CuisineViewModel cuisine)
        {
            try
            {
                await cuisineService.UpdateCuisine(cuisine);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Failed to update the cuisine: {GetFullErrorMessage(ex)}");
                return View(cuisine);
            }
        }

        //delete cuisine
        [HttpGet]
        public IActionResult Delete(string id)
        {
            try
            {
                cuisineService.DeleteCuisine(id);
                return RedirectToAction("DeletionComplete");
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred while deleting the cuisine: {GetFullErrorMessage(ex)}");
                return RedirectToAction(nameof(Index));
            }
        }

        //get recipes by cuisine
        [HttpGet]
        public IActionResult GetRecipesByCuisine(string id)
        {
            try
            {
                List<RecipeViewModel> cuisineRecipes = cuisineService.GetRecipesByCuisine(id);
                return View(cuisineRecipes);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred while retrieving cuisine recipes: {GetFullErrorMessage(ex)}");
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
