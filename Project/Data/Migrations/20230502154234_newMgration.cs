using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.Data.Migrations
{
    public partial class newMgration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipe_User_Recipe_AuthorUser_Username",
                table: "Recipe");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Recipe",
                table: "Recipe");

            migrationBuilder.DropIndex(
                name: "IX_Recipe_Recipe_AuthorUser_Username",
                table: "Recipe");

            migrationBuilder.DropColumn(
                name: "Recipe_Title",
                table: "Recipe");

            migrationBuilder.RenameColumn(
                name: "User_Id",
                table: "Review",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "Review_Rating",
                table: "Review",
                newName: "ReviewRating");

            migrationBuilder.RenameColumn(
                name: "Review_Id",
                table: "Review",
                newName: "ReviewId");

            migrationBuilder.RenameColumn(
                name: "Review_Date",
                table: "Review",
                newName: "ReviewDate");

            migrationBuilder.RenameColumn(
                name: "Recipe_Id",
                table: "Review",
                newName: "RecipeId");

            migrationBuilder.RenameColumn(
                name: "Review_Message",
                table: "Review",
                newName: "ReviewMessage");

            migrationBuilder.RenameColumn(
                name: "Recipe_Video",
                table: "Recipe",
                newName: "RecipeVideo");

            migrationBuilder.RenameColumn(
                name: "Recipe_Servings",
                table: "Recipe",
                newName: "RecipeServings");

            migrationBuilder.RenameColumn(
                name: "Recipe_Introduction",
                table: "Recipe",
                newName: "RecipeIntroduction");

            migrationBuilder.RenameColumn(
                name: "Recipe_Inredients",
                table: "Recipe",
                newName: "RecipeInredients");

            migrationBuilder.RenameColumn(
                name: "Recipe_Id",
                table: "Recipe",
                newName: "RecipeId");

            migrationBuilder.RenameColumn(
                name: "Recipe_Directions",
                table: "Recipe",
                newName: "RecipeDirections");

            migrationBuilder.RenameColumn(
                name: "Recipe_Description",
                table: "Recipe",
                newName: "RecipeDescription");

            migrationBuilder.RenameColumn(
                name: "Recipe_CookTime",
                table: "Recipe",
                newName: "RecipeCookTime");

            migrationBuilder.RenameColumn(
                name: "Recipe_Calories",
                table: "Recipe",
                newName: "RecipeCalories");

            migrationBuilder.RenameColumn(
                name: "Recipe_AuthorUser_Username",
                table: "Recipe",
                newName: "RecipeTitle");

            migrationBuilder.AddColumn<string>(
                name: "RecipeAuthorId",
                table: "Recipe",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserCity",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Recipe",
                table: "Recipe",
                column: "RecipeTitle");

            migrationBuilder.CreateIndex(
                name: "IX_Recipe_RecipeAuthorId",
                table: "Recipe",
                column: "RecipeAuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Recipe_AspNetUsers_RecipeAuthorId",
                table: "Recipe",
                column: "RecipeAuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipe_AspNetUsers_RecipeAuthorId",
                table: "Recipe");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Recipe",
                table: "Recipe");

            migrationBuilder.DropIndex(
                name: "IX_Recipe_RecipeAuthorId",
                table: "Recipe");

            migrationBuilder.DropColumn(
                name: "RecipeAuthorId",
                table: "Recipe");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserCity",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Review",
                newName: "User_Id");

            migrationBuilder.RenameColumn(
                name: "ReviewRating",
                table: "Review",
                newName: "Review_Rating");

            migrationBuilder.RenameColumn(
                name: "ReviewId",
                table: "Review",
                newName: "Review_Id");

            migrationBuilder.RenameColumn(
                name: "ReviewDate",
                table: "Review",
                newName: "Review_Date");

            migrationBuilder.RenameColumn(
                name: "RecipeId",
                table: "Review",
                newName: "Recipe_Id");

            migrationBuilder.RenameColumn(
                name: "ReviewMessage",
                table: "Review",
                newName: "Review_Message");

            migrationBuilder.RenameColumn(
                name: "RecipeVideo",
                table: "Recipe",
                newName: "Recipe_Video");

            migrationBuilder.RenameColumn(
                name: "RecipeServings",
                table: "Recipe",
                newName: "Recipe_Servings");

            migrationBuilder.RenameColumn(
                name: "RecipeIntroduction",
                table: "Recipe",
                newName: "Recipe_Introduction");

            migrationBuilder.RenameColumn(
                name: "RecipeInredients",
                table: "Recipe",
                newName: "Recipe_Inredients");

            migrationBuilder.RenameColumn(
                name: "RecipeId",
                table: "Recipe",
                newName: "Recipe_Id");

            migrationBuilder.RenameColumn(
                name: "RecipeDirections",
                table: "Recipe",
                newName: "Recipe_Directions");

            migrationBuilder.RenameColumn(
                name: "RecipeDescription",
                table: "Recipe",
                newName: "Recipe_Description");

            migrationBuilder.RenameColumn(
                name: "RecipeCookTime",
                table: "Recipe",
                newName: "Recipe_CookTime");

            migrationBuilder.RenameColumn(
                name: "RecipeCalories",
                table: "Recipe",
                newName: "Recipe_Calories");

            migrationBuilder.RenameColumn(
                name: "RecipeTitle",
                table: "Recipe",
                newName: "Recipe_AuthorUser_Username");

            migrationBuilder.AddColumn<string>(
                name: "Recipe_Title",
                table: "Recipe",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Recipe",
                table: "Recipe",
                column: "Recipe_Title");

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Role_Type = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Role_Id = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Role_Type);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    User_Username = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    User_Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    User_Id = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    User_Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    User_Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.User_Username);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Recipe_Recipe_AuthorUser_Username",
                table: "Recipe",
                column: "Recipe_AuthorUser_Username");

            migrationBuilder.AddForeignKey(
                name: "FK_Recipe_User_Recipe_AuthorUser_Username",
                table: "Recipe",
                column: "Recipe_AuthorUser_Username",
                principalTable: "User",
                principalColumn: "User_Username",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
