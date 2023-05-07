using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.Data.Migrations
{
    public partial class newMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Review",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "RecipeId",
                table: "Review",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "RecipeCategoryId",
                table: "Recipe",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RecipeCategory",
                columns: table => new
                {
                    RecipeCategoryId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RecipeCategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeCategory", x => x.RecipeCategoryId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Review_RecipeId",
                table: "Review",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_Review_UserId",
                table: "Review",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipe_RecipeCategoryId",
                table: "Recipe",
                column: "RecipeCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Recipe_RecipeCategory_RecipeCategoryId",
                table: "Recipe",
                column: "RecipeCategoryId",
                principalTable: "RecipeCategory",
                principalColumn: "RecipeCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Review_AspNetUsers_UserId",
                table: "Review",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Review_Recipe_RecipeId",
                table: "Review",
                column: "RecipeId",
                principalTable: "Recipe",
                principalColumn: "RecipeTitle",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipe_RecipeCategory_RecipeCategoryId",
                table: "Recipe");

            migrationBuilder.DropForeignKey(
                name: "FK_Review_AspNetUsers_UserId",
                table: "Review");

            migrationBuilder.DropForeignKey(
                name: "FK_Review_Recipe_RecipeId",
                table: "Review");

            migrationBuilder.DropTable(
                name: "RecipeCategory");

            migrationBuilder.DropIndex(
                name: "IX_Review_RecipeId",
                table: "Review");

            migrationBuilder.DropIndex(
                name: "IX_Review_UserId",
                table: "Review");

            migrationBuilder.DropIndex(
                name: "IX_Recipe_RecipeCategoryId",
                table: "Recipe");

            migrationBuilder.DropColumn(
                name: "RecipeCategoryId",
                table: "Recipe");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Review",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "RecipeId",
                table: "Review",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
