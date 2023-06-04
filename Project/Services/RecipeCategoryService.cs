using Project.Data;
using Project.Services.ViewModels;

namespace Project.Services
{
    public class RecipeCategoryService
    {
        private readonly ApplicationDbContext context;
        public RecipeCategoryService(ApplicationDbContext post)
        {
            context = post;
        }

        //get all categories
        public List<RecipeCategoryViewModel> GetAll()
        {
            return context.RecipeCategory.Select(recipeCategory => new RecipeCategoryViewModel()
            {
                RecipeCategoryId = recipeCategory.RecipeCategoryId,
                RecipeCategoryName = recipeCategory.RecipeCategoryName,
                RecipeCategoryImage = recipeCategory.RecipeCategoryImage,
            }).ToList();
        }
    }
}
