using Project.Data;
using Project.Services.ViewModels;

namespace Project.Services
{
    public class CuisineService
    {
        private readonly ApplicationDbContext context;
        public CuisineService(ApplicationDbContext post)
        {
            context = post;
        }

        //get all cuisines
        public List<CuisineViewModel> GetAll()
        {
            return context.Cuisine.Select(cuisine => new CuisineViewModel()
            {
                CuisineId = cuisine.CuisineId,
                CuisineName = cuisine.CuisineName,
                CuisineImage = cuisine.CuisineImage,
            }).ToList();
        }
    }
}