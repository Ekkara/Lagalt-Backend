using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lagalt_Backend.Migrations
{
    /// <inheritdoc />
    public partial class InitUpdateKeycloak : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "KeycloakId",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "KeycloakId",
                value: "20a04c91-29d4-4603-a640-e94908d22175");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "KeycloakId",
                value: "04b29fd4-862e-452f-a48d-d987a68c0104");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KeycloakId",
                table: "Users");
        }
    }
}
