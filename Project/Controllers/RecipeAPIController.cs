using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Project.Controllers.RecipeAPI;
using Project.Data;
using Project.Data.DataModels;
using Project.Interfaces;
using Project.Services;
using Project.Services.ViewModels;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace Project.Controllers
{
    public class RecipeAPIController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IRecipeService recipeService;
        private readonly HttpClient httpClient;
        private readonly UserManager<IdentityUser> _userManager;

        public RecipeAPIController(ApplicationDbContext dbContext, IRecipeService service, UserManager<IdentityUser> userManager)
        {
            context = dbContext;
            recipeService = service;
            _userManager = userManager;

            // Initialize HttpClient
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://api.spoonacular.com/"); // Fixed URL
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        private string GetFullErrorMessage(Exception ex)
        {
            string errorMessage = ex.Message;

            if (ex.InnerException != null)
            {
                errorMessage += " Inner Exception: " + ex.InnerException.Message;
            }

            return errorMessage;
        }

        public IActionResult Index()
        {
            return View();
        }

        //call API action
        public async Task<IActionResult> CallRecipeAPI(int numberOfRecipes, [Bind("RecipeCategoryId", "CuisineId")] RecipeViewModel recipeVM)
        {
            try
            {
                await AddRecipeFromAPI(numberOfRecipes, recipeVM.RecipeCategoryId, recipeVM.CuisineId, User);
                return RedirectToAction("Index", "Recipe");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"An error occurred while calling API: {GetFullErrorMessage(ex)}");
            }

            return RedirectToAction("Index", "Recipe");
        }

        //call API method
        public async Task AddRecipeFromAPI(int numberOfRecipes, string RecipeCategoryName, string CuisineName, ClaimsPrincipal user)
        {
            //spoonacular API
            RecipeCategoryName = RecipeCategoryName.ToLower();
            CuisineName = CuisineName.ToLower();
            string apiKey = "aa905d58c15e4d46b03ee46e5489520b";
            string endpoint = $"https://api.spoonacular.com/recipes/random?number={numberOfRecipes}&tags={RecipeCategoryName},{CuisineName}&apiKey={apiKey}";

            try
            {
                HttpResponseMessage response = await httpClient.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();

                string apiResponse = await response.Content.ReadAsStringAsync();
                var responseObject = JsonConvert.DeserializeObject<Root>(apiResponse);

                var userId = _userManager.GetUserId(user);
                var category = await context.RecipeCategory.FirstOrDefaultAsync(rc => rc.RecipeCategoryName == RecipeCategoryName);
                var cuisine = await context.Cuisine.FirstOrDefaultAsync(c => c.CuisineName == CuisineName);

                if (string.IsNullOrEmpty(userId))
                {
                    throw new ArgumentException("Invalid user ID.");
                }
                if (category == null)
                {
                    throw new ArgumentException("Invalid recipe category.");
                }
                if (cuisine == null)
                {
                    throw new ArgumentException("Invalid cuisine.");
                }

                foreach (var recipeData in responseObject.recipes)
                {
                    Recipe recipe = new Recipe
                    {
                        RecipeId = Guid.NewGuid().ToString(),
                        RecipeTitle = recipeData.title,
                        RecipeImage = recipeData.image,
                        RecipeInredients = string.Join(Environment.NewLine, recipeData.extendedIngredients.Select(i => $"{i.amount} {i.unit} of {i.name}")),
                        RecipeDescription = recipeData.summary,
                        RecipeIntroduction = "This recipe is pure deliciousness!",
                        RecipeDirections = recipeData.instructions,
                        RecipeCookTime = recipeData.readyInMinutes,
                        RecipeCalories = 200,
                        RecipeServings = recipeData.servings,
                        RecipeAuthorId = userId,
                        RecipeCategoryId = category.RecipeCategoryId,
                        CuisineId = cuisine.CuisineId,
                    };

                    category.RecipeCategoryRecipes ??= new List<Recipe>();
                    category.RecipeCategoryRecipes.Add(recipe);

                    cuisine.CuisineRecipes ??= new List<Recipe>();
                    cuisine.CuisineRecipes.Add(recipe);

                    await context.AddAsync(recipe);
                }

                await context.SaveChangesAsync();
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Adding recipes to the database has failed.", ex);
            }
        }
    }
}
