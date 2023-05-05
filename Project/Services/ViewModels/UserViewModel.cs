using System.ComponentModel.DataAnnotations;

namespace Project.Services.ViewModels
{
    public class UserViewModel
    {
        public string UserId { get; set; }
        [Key]
        public string UserUsername { get; set; }
        public string UserEmail { get; set; }
        public string UserPassword { get; set; }
        public string UserRole { get; set; }
    }
}
