using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Data.DataModels;
using Project.Interfaces;
using Project.Services.ViewModels;
using System.Security.Claims;

namespace Project.Services
{
    public class ReviewService: IReviewService
    {
        private readonly ApplicationDbContext context;
        //
        private readonly UserManager<IdentityUser> _userManager;

        public ReviewService(ApplicationDbContext post, UserManager<IdentityUser> userManager)
        {
            context = post;
            _userManager = userManager;
        }

        public async Task AddReview(ReviewViewModel review, string recipeId, ClaimsPrincipal user)
        {
            if (review == null)
            {
                throw new ArgumentNullException(nameof(review), "Review is invalid.");
            }

            var userId = _userManager.GetUserId(user);

            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentException("Invalid user ID.");
            }

            var reviewDb = new Review
            {
                ReviewId = Guid.NewGuid().ToString(),
                ReviewMessage = review.ReviewMessage,
                ReviewDate = DateTime.Now,
                RecipeId = recipeId,
                ReviewAuthorId = userId,
            };

            var recipe = await context.Recipe.FirstOrDefaultAsync(u => u.RecipeId == recipeId);
            if (recipe == null)
            {
                throw new Exception("Invalid recipe.");
            }

            if (recipe.RecipeReviews == null)
            {
                recipe.RecipeReviews = new List<Review>();
            }
            recipe.RecipeReviews.Add(reviewDb);

            context.Add(reviewDb);
            await context.SaveChangesAsync();
        }

        //delete review
        public async Task DeleteReview(string id)
        {
            if (string.IsNullOrEmpty(id) || string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException("Review ID is invalid.", nameof(id));
            }
            if (id != null)
            {
                var reviewDb = context.Review.FirstOrDefault(x => x.ReviewId == id);

                if (reviewDb == null)
                {
                    throw new ArgumentNullException(nameof(reviewDb), "Review not found.");
                }

                context.Review.Remove(reviewDb);
                await context.SaveChangesAsync();
            }
        }

    }
}
