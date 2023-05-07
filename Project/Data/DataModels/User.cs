using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Project.Data.DataModels
{
    public class User :IdentityUser
    {
        public string UserCity { get; set; }
        public ICollection<Recipe> UserRecipes { get; set; }

        public ICollection<Review> UserReviews { get; set; }
    }
}
