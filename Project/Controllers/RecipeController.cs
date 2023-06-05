using Microsoft.AspNetCore.Mvc;
using Project.Services;
using Project.Services.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors.Infrastructure;
using System.Net;
using Project.Data.DataModels;

namespace Project.Controllers
{
    public class RecipeController:Controller
    {
        public RecipeService recipeService { get; set; }
        public RecipeController(RecipeService service)
        {
            recipeService = service;
        }

        //index
        [HttpGet]
        public IActionResult Index()
        {
            List<RecipeViewModel> recipes = recipeService.GetAll();

            return this.View(recipes);
        }

        //details recipe
        [HttpGet]
        public IActionResult Details(string id)
        {
            RecipeViewModel recipe = recipeService.GetDetailsById(id);

            bool isCourseNull = recipe == null;
            if (isCourseNull)
            {
                return this.RedirectToAction("Index");
            }

            return this.View(recipe);
        }

        //add recipe
        [HttpGet]
        public IActionResult AddRecipe()
        {
            return View();
        }

        //add recipe
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRecipe([Bind("RecipeId", "RecipeTitle", "RecipeImage", "RecipeInredients", "RecipeDescription", "RecipeIntroduction", "RecipeDirections", "RecipeCookTime", "RecipeCalories", "RecipeServings", "RecipeServings", "RecipeAuthorId", "RecipeCategoryId", "CuisineId")] RecipeViewModel recipeVM)
        {
            await recipeService.AddRecipe(recipeVM);
            return RedirectToAction(nameof(Index));
        }

        //update recipe
        [HttpGet]
        public IActionResult Update(string id)
        {
            RecipeViewModel recipe = this.recipeService.UpdateById(id);
            return this.View(recipe);
        }

        //update recipe
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Update(RecipeViewModel recipe)
        {
            try
            {
                await recipeService.UpdateRecipe(recipe);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Failed to update the recipe: {ex.Message}");
                return View(recipe);
            }
        }

        //delete recipe
        [HttpGet]
        public IActionResult Delete(string id)
        {
            try
            {
                recipeService.DeleteRecipe(id);
                return RedirectToAction("Index");
            }
            catch (ArgumentException e)
            {
                ViewData["Message"] = e.Message;

                return RedirectToAction(nameof(Index));
            }
        }

        //get reviews of a recipe
        [HttpGet]
        public IActionResult GetRecipeReviews(string id)
        {
            List<ReviewViewModel> recipereviews = recipeService.GetRecipeReviews(id);
            return this.View(recipereviews);
        }
    }
}
