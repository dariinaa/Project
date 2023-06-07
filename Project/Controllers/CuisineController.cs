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

        //index
        [Authorize]
        public IActionResult Index()
        {
            List<CuisineViewModel> cuisines = cuisineService.GetAll();

            return this.View(cuisines);
        }

        //cuisine category
        [HttpGet]
        public IActionResult Details(string id)
        {
            CuisineViewModel cuisine = cuisineService.GetDetailsById(id);

            bool isCourseNull = cuisine == null;
            if (isCourseNull)
            {
                return this.RedirectToAction("Index");
            }

            return this.View(cuisine);
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
            await cuisineService.AddCuisine(cuisineVM);
            return RedirectToAction(nameof(Index));
        }

        //update cuisine
        [HttpGet]
        public IActionResult Update(string id)
        {
            CuisineViewModel cuisine = this.cuisineService.GetDetailsById(id);
            return this.View(cuisine);
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
                ModelState.AddModelError("", $"Failed to update the recipe: {ex.Message}");
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
                return RedirectToAction("Index");
            }
            catch (ArgumentException e)
            {
                ViewData["Message"] = e.Message;

                return RedirectToAction(nameof(Index));
            }
        }

        //get recipes by cuisine
        public IActionResult GetRecipesByCuisine(string id)
        {
            List<RecipeViewModel> cuisineRecipes = cuisineService.GetRecipesByCuisine(id);

            return this.View(cuisineRecipes);
        }
    }
}
