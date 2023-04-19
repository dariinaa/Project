using System.ComponentModel.DataAnnotations;

namespace Project.Data.DataModels
{
    public class Recipe
    {
        public string Recipe_Id { get; set; }
        [Key]
        public string Recipe_Title { get; set; }
        //public IFormFile Recipe_Image { get; set; }
        public string Recipe_Video { get; set; }
        public string Recipe_Inredients { get; set; }
        public string Recipe_Description { get; set; }
        public User Recipe_Author { get; set; }
        public string Recipe_Introduction { get; set; }
        public string Recipe_Directions { get; set; }
        public double Recipe_CookTime { get; set; }
        public double Recipe_Calories { get; set; }
        public int Recipe_Servings { get; set; }

    }
}
