using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Data.DataModels;
using Project.Interfaces;
using Project.Services;
using Project.Services.ViewModels;

namespace Project.Controllers
{
    public class RecipeCategoryController : Controller
    {
        public IRecipeCategoryService recipeCategoryService { get; set; }
        public RecipeCategoryController(IRecipeCategoryService service)
        {
            recipeCategoryService = service;
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
                List<RecipeCategoryViewModel> recipeCategories = recipeCategoryService.GetAll();
                return View(recipeCategories);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred while retrieving recipe categories: {GetFullErrorMessage(ex)}");
                return View();
            }
        }

        //deletion complete
        public IActionResult DeletionComplete()
        {
            return this.View();
        }

        //details recipe category
        [HttpGet]
        public IActionResult Details(string id)
        {
            try
            {
                RecipeCategoryViewModel recipeCategory = recipeCategoryService.GetDetailsById(id);
                return View(recipeCategory);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred while retrieving recipe category details: {GetFullErrorMessage(ex)}");
                return View();
            }
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
        public async Task<IActionResult> AddRecipeCategory([Bind("RecipeCategoryName", "RecipeCategoryImage")] RecipeCategoryViewModel recipeCategoryVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await recipeCategoryService.AddRecipeCategory(recipeCategoryVM);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, $"An error occurred while adding the recipe category: {GetFullErrorMessage(ex)}");
                    return View(recipeCategoryVM);
                }
            }
            return View(recipeCategoryVM);
        }

        //update recipe category
        [HttpGet]
        public IActionResult Update(string id)
        {
            try
            {
                RecipeCategoryViewModel recipeCategory = recipeCategoryService.GetDetailsById(id);
                return View(recipeCategory);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred while retrieving recipe category details: {GetFullErrorMessage(ex)}");
                return View();
            }
        }

        //update recipe category
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Update(RecipeCategoryViewModel recipeCategory)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await recipeCategoryService.UpdateRecipeCategory(recipeCategory);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, $"An error occurred while updating the recipe category: {GetFullErrorMessage(ex)}");
                    return View(recipeCategory);
                }
            }
            return View(recipeCategory);
        }

        //delete recipe category
        [HttpGet]
        public IActionResult Delete(string id)
        {
            try
            {
                recipeCategoryService.DeleteRecipeCategory(id);
                return RedirectToAction("DeletionComplete");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred while deleting the recipe category: {GetFullErrorMessage(ex)}");
                return RedirectToAction(nameof(Index));
            }
        }

        //get recipes by category
        [HttpGet]
        public IActionResult GetRecipesByCategory(string id)
        {
            try
            {
                List<RecipeViewModel> recipeCategoryRecipes = recipeCategoryService.GetRecipesByCategory(id);
                return View(recipeCategoryRecipes);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred while retrieving recipes by category: {GetFullErrorMessage(ex)}");
                return View();
            }
        }
    }
}
