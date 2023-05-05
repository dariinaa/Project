﻿using System.ComponentModel.DataAnnotations;

namespace Project.Data.DataModels
{
    public class Recipe
    {
        public string RecipeId { get; set; }
        [Key]
        public string RecipeTitle { get; set; }
        //public IFormFile Recipe_Image { get; set; }
        public string RecipeVideo { get; set; }
        public string RecipeInredients { get; set; }
        public string RecipeDescription { get; set; }
        public User RecipeAuthor { get; set; }
        public string RecipeIntroduction { get; set; }
        public string RecipeDirections { get; set; }
        public double RecipeCookTime { get; set; }
        public double RecipeCalories { get; set; }
        public int RecipeServings { get; set; }

        //public ListReview MyProperty { get; set; }

    }
}
