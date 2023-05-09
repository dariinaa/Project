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
            List<RecipeViewModel> movies = recipeService.GetAll();

            return this.View(movies);
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
            RecipeViewModel movie = this.recipeService.UpdateById(id);

            return this.View(movie);
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
    }
}
