using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Review",
                columns: table => new
                {
                    Review_Message = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Review_Id = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Review_Rating = table.Column<double>(type: "float", nullable: false),
                    Review_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Recipe_Id = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    User_Id = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Review", x => x.Review_Message);
                });

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
                    User_Id = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    User_Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    User_Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    User_Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.User_Username);
                });

            migrationBuilder.CreateTable(
                name: "Recipe",
                columns: table => new
                {
                    Recipe_Title = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Recipe_Id = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Recipe_Video = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Recipe_Inredients = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Recipe_Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Recipe_AuthorUser_Username = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Recipe_Introduction = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Recipe_Directions = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Recipe_CookTime = table.Column<double>(type: "float", nullable: false),
                    Recipe_Calories = table.Column<double>(type: "float", nullable: false),
                    Recipe_Servings = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipe", x => x.Recipe_Title);
                    table.ForeignKey(
                        name: "FK_Recipe_User_Recipe_AuthorUser_Username",
                        column: x => x.Recipe_AuthorUser_Username,
                        principalTable: "User",
                        principalColumn: "User_Username",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Recipe_Recipe_AuthorUser_Username",
                table: "Recipe",
                column: "Recipe_AuthorUser_Username");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Recipe");

            migrationBuilder.DropTable(
                name: "Review");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
