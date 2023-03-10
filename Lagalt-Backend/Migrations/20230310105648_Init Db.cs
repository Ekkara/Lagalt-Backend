using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Lagalt_Backend.Migrations
{
    /// <inheritdoc />
    public partial class InitDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProjectDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProjectCategoryId = table.Column<int>(type: "int", nullable: false),
                    ProjectCategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProjectIsAvailable = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Hidden = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "Id", "ProjectCategoryId", "ProjectCategoryName", "ProjectDescription", "ProjectIsAvailable", "ProjectName" },
                values: new object[,]
                {
                    { 1, 1, "Games", "World-Class MMORPG", true, "Final Fantasy XIV" },
                    { 2, 2, "Music", "Rock song doubling as a boss theme", false, "Scream" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Hidden", "UserName" },
                values: new object[,]
                {
                    { 1, true, "Maddie" },
                    { 2, false, "Alice" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
