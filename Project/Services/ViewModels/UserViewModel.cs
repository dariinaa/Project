using Project.Data.DataModels;
using System.ComponentModel.DataAnnotations;

namespace Project.Services.ViewModels
{
    public class UserViewModel
    {
        public string UserCity { get; set; }
        public ICollection<Recipe> UserRecipes { get; set; }

        public ICollection<Review> UserReviews { get; set; }
    }
}
