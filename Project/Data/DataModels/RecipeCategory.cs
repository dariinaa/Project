namespace Project.Data.DataModels
{
    public class RecipeCategory
    {
        public string RecipeCategoryId { get; set; }
        public string RecipeCategoryName { get; set; }

        public ICollection<Recipe> RecipeCategoryRecipes { get; set; }
    }
}
