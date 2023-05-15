using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Index()
        {
            List<RecipeCategoryViewModel> recipeCategories = recipeCategoryService.GetAll();

            return this.View(recipeCategories);
        }
    }
}
