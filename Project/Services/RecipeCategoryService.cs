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
        public List<RecipeCategoryViewModel> GetAll()
        {
            return context.RecipeCategory.Select(recipeCategory => new RecipeCategoryViewModel()
            {
                RecipeCategoryName = recipeCategory.RecipeCategoryName,
            }).ToList();
        }
    }
}
