using System.ComponentModel.DataAnnotations;

namespace Project.Data.DataModels
{
    public class User
    {
        public string User_Id { get; set; }
        [Key]
        public string User_Username { get; set; }
        public string User_Email { get; set; }
        public string User_Password { get; set; }
        public string User_Role { get; set; }
    }
}
