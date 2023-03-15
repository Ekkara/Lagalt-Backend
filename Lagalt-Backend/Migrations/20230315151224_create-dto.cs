using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lagalt_Backend.Migrations
{
    /// <inheritdoc />
    public partial class createdto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProjectIsAvailable",
                table: "Projects",
                newName: "IsAvailable");

            migrationBuilder.RenameColumn(
                name: "ProjectDescription",
                table: "Projects",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "ProjectCategoryName",
                table: "Projects",
                newName: "CategoryName");

            migrationBuilder.RenameColumn(
                name: "ProjectCategoryId",
                table: "Projects",
                newName: "OwnerId");

            migrationBuilder.AddColumn<string>(
                name: "DummyData1",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DummyData2",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DummyData3",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DummyData1", "DummyData2", "DummyData3", "OwnerId" },
                values: new object[] { null, null, null, 0 });

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DummyData1", "DummyData2", "DummyData3", "OwnerId" },
                values: new object[] { null, null, null, 0 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DummyData1",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "DummyData2",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "DummyData3",
                table: "Projects");

            migrationBuilder.RenameColumn(
                name: "OwnerId",
                table: "Projects",
                newName: "ProjectCategoryId");

            migrationBuilder.RenameColumn(
                name: "IsAvailable",
                table: "Projects",
                newName: "ProjectIsAvailable");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Projects",
                newName: "ProjectDescription");

            migrationBuilder.RenameColumn(
                name: "CategoryName",
                table: "Projects",
                newName: "ProjectCategoryName");

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 1,
                column: "ProjectCategoryId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 2,
                column: "ProjectCategoryId",
                value: 2);
        }
    }
}
