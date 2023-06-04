using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Project.Data.DataModels;

namespace Project.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        public DbSet<Recipe> Recipe { get; set; }
        public DbSet<Review> Review { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<RecipeCategory> RecipeCategory { get; set; }
        public DbSet<Cuisine> Cuisine { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            builder.Entity<Recipe>()
               .HasOne<Cuisine>(x => x.Cuisine)
               .WithMany(y => y.CuisineRecipes)
               .HasForeignKey(z => z.CuisineId)
               .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Recipe>()
                .HasOne<RecipeCategory>(x => x.RecipeCategory)
                .WithMany(y => y.RecipeCategoryRecipes)
                .HasForeignKey(z => z.RecipeCategoryId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Recipe>()
                .HasOne<User>(x => x.User)
                .WithMany(y => y.UserRecipes)
                .HasForeignKey(z => z.RecipeAuthorId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Review>()
                .HasOne<Recipe>(x => x.Recipe)
                .WithMany(y => y.RecipeReviews)
                .HasForeignKey(z => z.RecipeId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Review>()
                .HasOne<User>(x => x.User)
                .WithMany(y => y.UserReviews)
                .HasForeignKey(z => z.ReviewAuthorId)
                .OnDelete(DeleteBehavior.NoAction);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-SDA3RDF\\SQLEXPRESS;Database=ProjectDB6;Trusted_Connection=True;TrustServerCertificate=True;");
        }
    }
}