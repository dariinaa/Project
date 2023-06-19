using Microsoft.AspNetCore.Mvc;
using Project.Services;
using Project.Services.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors.Infrastructure;
using System.Net;
using Project.Data.DataModels;
using Microsoft.AspNetCore.Authorization;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Project.Controllers.RecipeAPI;
using Project.Interfaces;

namespace Project.Controllers
{
    public class RecipeController:Controller
    {
        public IRecipeService recipeService { get; set; }
        public RecipeController(IRecipeService service)
        {
            recipeService = service;     
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
        [Authorize]
        public IActionResult Index()
        {
            try
            {
                List<RecipeViewModel> recipes = recipeService.GetAll();
                return View(recipes);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"An error occurred while retrieving recipes: {GetFullErrorMessage(ex)}");
                return View();
            }
        }

        //deletion complete
        public IActionResult DeletionComplete()
        {
            return this.View();
        }

        //add recipe using API
        public IActionResult AddRecipeAPI()
        {
            return this.View();
        }

        //details recipe
        [HttpGet]
        public IActionResult Details(string id)
        {
            try
            {
                RecipeViewModel recipe = recipeService.GetDetailsById(id);
                return View(recipe);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"An error occurred while retrieving recipe details: {GetFullErrorMessage(ex)}");
                return View();
            }
        }

        //add recipe
        [HttpGet]
        public IActionResult AddRecipe()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRecipe([Bind("RecipeTitle", "RecipeImage", "RecipeInredients", "RecipeDescription", "RecipeIntroduction", "RecipeDirections", "RecipeCookTime", "RecipeCalories", "RecipeServings", "RecipeServings", "RecipeCategoryName", "CuisineName")] RecipeViewModel recipeVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await recipeService.AddRecipe(recipeVM, User);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"An error occurred while adding the recipe: {GetFullErrorMessage(ex)}");
                    return View(recipeVM);
                }
            }
            return View(recipeVM);
        }

        //update recipe
        [HttpGet]
        public IActionResult Update(string id)
        {
            try
            {
                RecipeViewModel recipe = recipeService.UpdateById(id);
                return View(recipe);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"An error occurred while retrieving recipe for update: {GetFullErrorMessage(ex)}");
                return View();
            }
        }

        //update recipe
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Update([Bind("RecipeId", "RecipeTitle", "RecipeImage", "RecipeInredients", "RecipeDescription", "RecipeIntroduction", "RecipeDirections", "RecipeCookTime", "RecipeCalories", "RecipeServings", "RecipeServings", "RecipeCategoryName", "CuisineName")] RecipeViewModel recipe)
        {
            if (ModelState.IsValid)
            {
                try
               {
                    await recipeService.UpdateRecipe(recipe);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Failed to update the recipe: {GetFullErrorMessage(ex)}");
                    return View(recipe);
                }
            }
            return View(recipe);
        }

        //delete recipe
        [HttpGet]
        public IActionResult Delete(string id)
        {
            try
            {
                recipeService.DeleteRecipe(id);
                return RedirectToAction("DeletionComplete");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"An error occurred while deleting the recipe: {GetFullErrorMessage(ex)}");
                return RedirectToAction(nameof(Index));
            }
        }

        //get reviews of a recipe
        [HttpGet]
        public IActionResult GetRecipeReviews(string id)
        {
            try
            {
                List<ReviewViewModel> recipereviews = recipeService.GetRecipeReviews(id);
                return View(recipereviews);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"An error occurred while retrieving recipe reviews: {GetFullErrorMessage(ex)}");
                return View();
            }
        }

        //search bar
        [HttpGet]
        public IActionResult Search(string query)
        {
            List<RecipeViewModel> searchResults = recipeService.SearchRecipes(query);

            return View(searchResults);
        }
    }
}
