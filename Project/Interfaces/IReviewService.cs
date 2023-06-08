using Project.Services.ViewModels;
using System.Security.Claims;

namespace Project.Interfaces
{
    public interface IReviewService
    {
        Task AddReview(ReviewViewModel review, string recipeId, ClaimsPrincipal user);
        Task DeleteReview(string id);
    }
}
