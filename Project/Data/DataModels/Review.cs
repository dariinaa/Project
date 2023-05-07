using System.ComponentModel.DataAnnotations;

namespace Project.Data.DataModels
{
    public class Review
    {
        [Key]
        public string ReviewId { get; set; }
        public string ReviewMessage { get; set; }
        public double ReviewRating { get; set; }
        public DateTime ReviewDate { get; set; }
        public string RecipeId { get; set; }
        public Recipe Recipe { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
