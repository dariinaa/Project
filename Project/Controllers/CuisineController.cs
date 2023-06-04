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
        public IActionResult Index()
        {
            List<CuisineViewModel> cuisines = cuisineService.GetAll();

            return this.View(cuisines);
        }
    }
}
