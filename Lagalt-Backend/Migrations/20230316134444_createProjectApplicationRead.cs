using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lagalt_Backend.Migrations
{
    /// <inheritdoc />
    public partial class createProjectApplicationRead : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectApplication_Projects_ProjectId",
                table: "ProjectApplication");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectApplication",
                table: "ProjectApplication");

            migrationBuilder.RenameTable(
                name: "ProjectApplication",
                newName: "ProjectApplications");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectApplication_ProjectId",
                table: "ProjectApplications",
                newName: "IX_ProjectApplications_ProjectId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectApplications",
                table: "ProjectApplications",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectApplications_Projects_ProjectId",
                table: "ProjectApplications",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectApplications_Projects_ProjectId",
                table: "ProjectApplications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectApplications",
                table: "ProjectApplications");

            migrationBuilder.RenameTable(
                name: "ProjectApplications",
                newName: "ProjectApplication");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectApplications_ProjectId",
                table: "ProjectApplication",
                newName: "IX_ProjectApplication_ProjectId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectApplication",
                table: "ProjectApplication",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectApplication_Projects_ProjectId",
                table: "ProjectApplication",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
