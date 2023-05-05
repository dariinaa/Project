using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Project.Data.DataModels
{
    public class User :IdentityUser
    {
        public string UserCity { get; set; }
    }
}
