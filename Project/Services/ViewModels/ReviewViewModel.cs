using System.ComponentModel.DataAnnotations;

namespace Project.Services.ViewModels
{
    public class ReviewViewModel
    {
        public string ReviewId { get; set; }
        [Key]
        public string ReviewMessage { get; set; }
        public double ReviewRating { get; set; }
        public DateTime ReviewDate { get; set; }
        public string RecipeId { get; set; }
        public string UserId { get; set; }
    }
}
