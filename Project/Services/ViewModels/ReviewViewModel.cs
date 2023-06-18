using Project.Data.DataModels;
using System.ComponentModel.DataAnnotations;

namespace Project.Services.ViewModels
{
    public class ReviewViewModel
    {
        [Key]
        public string? ReviewId { get; set; }
        public string ReviewMessage { get; set; }
        public DateTime? ReviewDate { get; set; }

        public string? RecipeId { get; set; }
        public Recipe? Recipe { get; set; }

        public string? ReviewAuthorName { get; set; }
        public string? ReviewAuthorId { get; set; }
        public User? User { get; set; }
    }
}
