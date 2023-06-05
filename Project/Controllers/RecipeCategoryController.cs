using Microsoft.AspNetCore.Mvc;
using Project.Data.DataModels;
using Project.Services;
using Project.Services.ViewModels;

namespace Project.Controllers
{
    public class RecipeCategoryController : Controller
    {
        public RecipeCategoryService recipeCategoryService { get; set; }
        public RecipeCategoryController(RecipeCategoryService service)
        {
            recipeCategoryService = service;
        }

        //index
        public IActionResult Index()
        {
            List<RecipeCategoryViewModel> recipeCategories = recipeCategoryService.GetAll();

            return this.View(recipeCategories);
        }

        //details recipe category
        [HttpGet]
        public IActionResult Details(string id)
        {
            RecipeCategoryViewModel recipeCategory = recipeCategoryService.GetDetailsById(id);

            bool isCourseNull = recipeCategory == null;
            if (isCourseNull)
            {
                return this.RedirectToAction("Index");
            }

            return this.View(recipeCategory);
        }

        //add recipe category
        [HttpGet]
        public IActionResult AddRecipeCategory()
        {
            return View();
        }

        //add recipe category
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRecipeCategory([Bind("RecipeCategoryId", "RecipeCategoryName", "RecipeCategoryImage")] RecipeCategoryViewModel recipeCategoryVM)
        {
            await recipeCategoryService.AddRecipeCategory(recipeCategoryVM);
            return RedirectToAction(nameof(Index));
        }

        //update recipe category
        [HttpGet]
        public IActionResult Update(string id)
        {
            RecipeCategoryViewModel recipeCategory = this.recipeCategoryService.GetDetailsById(id);
            return this.View(recipeCategory);
        }

        //update recipe category
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Update(RecipeCategoryViewModel recipeCategory)
        {
            try
            {
                await recipeCategoryService.UpdateRecipeCategory(recipeCategory);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Failed to update the recipe: {ex.Message}");
                return View(recipeCategory);
            }
        }

        //delete recipe category
        [HttpGet]
        public IActionResult Delete(string id)
        {
            try
            {
                recipeCategoryService.DeleteRecipeCategory(id);
                return RedirectToAction("Index");
            }
            catch (ArgumentException e)
            {
                ViewData["Message"] = e.Message;

                return RedirectToAction(nameof(Index));
            }
        }

        //get recipes by category
        public IActionResult GetRecipesByCategory(string id)
        {
            List<RecipeViewModel> recipeCategoryRecipes = recipeCategoryService.GetRecipesByCategory(id);
            return this.View(recipeCategoryRecipes);
        }
    }
}
