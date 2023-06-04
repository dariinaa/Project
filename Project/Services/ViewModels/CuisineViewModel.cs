using Project.Data.DataModels;
using System.ComponentModel.DataAnnotations;

namespace Project.Services.ViewModels
{
    public class CuisineViewModel
    {
        [Key]
        public string CuisineId { get; set; }
        public string CuisineName { get; set; }
        public string CuisineImage { get; set; }

        public ICollection<Recipe> CuisineRecipes { get; set; }
    }
}
