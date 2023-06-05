using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Data.DataModels;
using Project.Services.ViewModels;

namespace Project.Services
{
    public class ReviewService
    {
        private readonly ApplicationDbContext context;
        public ReviewService(ApplicationDbContext post)
        {
            context = post;
        }

        //add review
        public async Task AddReview(ReviewViewModel review, string recipeId)
        {
            var recipe = await context.Recipe.FirstOrDefaultAsync(u => u.RecipeId == recipeId);
            var author = await context.Users.FirstOrDefaultAsync(u => u.UserName == review.ReviewAuthorId);
            if (author == null)
            {
                throw new Exception("Invalid author name.");
            }
            var reviewDb = new Review
            {
                ReviewId = Guid.NewGuid().ToString(),
                ReviewMessage = review.ReviewMessage,
                ReviewDate = review.ReviewDate,
                RecipeId = recipeId,
                ReviewAuthorId = author.Id,
            };
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
                Console.WriteLine("Eror!");
            }
            if (id != null)
            {
                var reviewDb = context.Review.FirstOrDefault(x => x.ReviewId == id);
                context.Review.Remove(reviewDb);
                await context.SaveChangesAsync();
            }
        }

    }
}
