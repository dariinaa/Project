using Microsoft.AspNetCore.Mvc;
using Project.Interfaces;
using Project.Services;
using Project.Services.ViewModels;

namespace Project.Controllers
{
    public class ReviewController : Controller
    {
        public IReviewService reviewService { get; set; }
        public ReviewController(IReviewService service)
        {
            reviewService = service;
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

        //addition complete
        public IActionResult AdditionComplete()
        {
            return this.View();
        }

        //deletion complete
        public IActionResult DeletionComplete()
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
        public async Task<IActionResult> AddReview([Bind("ReviewMessage")] ReviewViewModel reviewVM, string id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await reviewService.AddReview(reviewVM, id, User);
                    return this.RedirectToAction("AdditionComplete");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"An error occurred while adding review: {GetFullErrorMessage(ex)}");
                    return View();
                }
            }
            return View();
        }

        //delete review
        [HttpGet]
        public IActionResult Delete(string id)
        {
            try
            {
                reviewService.DeleteReview(id);
                return RedirectToAction("DeletionComplete");
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError("", $"An error occurred while deleting review: {GetFullErrorMessage(ex)}");
                return RedirectToAction(nameof(Index), "Recipe");
            }
        }

    }
}
