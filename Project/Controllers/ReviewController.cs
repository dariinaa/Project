using Microsoft.AspNetCore.Mvc;
using Project.Services;
using Project.Services.ViewModels;

namespace Project.Controllers
{
    public class ReviewController : Controller
    {
        public ReviewService reviewService { get; set; }
        public ReviewController(ReviewService service)
        {
            reviewService = service;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        public IActionResult DeleteConfirmed()
        {
            return this.View();
        }

        //add review
        [HttpGet]
        public IActionResult AddReview()
        {
            return View();
        }

        //add review
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddReview([Bind("ReviewId", "ReviewMessage", "ReviewDate", "RecipeId", "ReviewAuthorId")] ReviewViewModel reviewVM, string id)
        {
            try
            {
            await reviewService.AddReview(reviewVM, id);
            return this.RedirectToAction("Index");
            }
            catch (Exception ex)
            {
             return View("Error");
            }
        }

        //delete review
        [HttpGet]
        public IActionResult Delete(string id)
        {
            try
            {
                reviewService.DeleteReview(id);
                return RedirectToAction("Index");
            }
            catch (ArgumentException e)
            {
                ViewData["Message"] = e.Message;

                return RedirectToAction(nameof(Index));
            }
        }

    }
}
