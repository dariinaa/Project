using Microsoft.AspNetCore.Mvc;
using Project.Services;
using Project.Services.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors.Infrastructure;
using System.Net;

namespace Project.Controllers
{
    public class RecipeController:Controller
    {
        public RecipeService recipeService { get; set; }
        public RecipeController(RecipeService service)
        {
            recipeService = service;
        }
        [HttpGet]
        public IActionResult Index()
        {
            List<RecipeViewModel> recipes = recipeService.GetAll();

            return this.View(recipes);
        }
        [HttpGet]
        public IActionResult AddRecipe()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRecipe([Bind("RecipeId", "RecipeTitle", "RecipeVideo", "RecipeInredients", "RecipeDescription", "RecipeAuthor", "RecipeIntroduction", "RecipeDirections", "RecipeCookTime", "RecipeCalories", "RecipeServings")] RecipeViewModel recipeVM)
        {
            await recipeService.AddRecipe(recipeVM);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Update(string id)
        {
            RecipeViewModel recipe = this.recipeService.UpdateById(id);

            return this.View(recipe);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Update(RecipeViewModel model)
        {
            if (this.ModelState.IsValid == false)
            {
                return this.View(model);
            }

            await this.recipeService.UpdateAsync(model);

            return this.RedirectToAction("index");
        }
        [HttpGet]
        public IActionResult Delete(string id)
        {
            try
            {
                RecipeViewModel recipe = recipeService.GetDetailsById(id);

                return View(recipe);
            }
            catch (ArgumentException e)
            {
                ViewData["Message"] = e.Message;

                return RedirectToAction(nameof(Index));
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmation(string id)
        {
            try
            {
                Console.WriteLine(id);
                await recipeService.DeleteRecipe(id);

                return RedirectToAction(nameof(Index));
            }
            catch (ArgumentException e)
            {
                ViewData["Message"] = e.Message;

                return RedirectToAction(nameof(Index));
            }
        }
    }
}
