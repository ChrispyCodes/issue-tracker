using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IssueTrackingSystem.Data.Migrations
{
    public partial class UpdateIssueKeyName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Issues",
                newName: "IssueId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IssueId",
                table: "Issues",
                newName: "Id");
        }
    }
}
