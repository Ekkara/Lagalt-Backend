using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Lagalt_Backend.Migrations
{
    /// <inheritdoc />
    public partial class seeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "Id", "ProjectCategoryId", "ProjectCategoryName", "ProjectName" },
                values: new object[,]
                {
                    { 1, 1, "Games", "My New Game" },
                    { 2, 2, "Music", "My Awesome Song" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Hidden", "UserName" },
                values: new object[,]
                {
                    { 1, true, "Maddie" },
                    { 2, false, "Davis" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
