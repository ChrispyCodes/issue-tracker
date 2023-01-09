using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IssueTrackingSystem.Data.Migrations
{
    public partial class AddUserIdAsFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "Issues",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Issues_Id",
                table: "Issues",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_AspNetUsers_Id",
                table: "Issues",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Issues_AspNetUsers_Id",
                table: "Issues");

            migrationBuilder.DropIndex(
                name: "IX_Issues_Id",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Issues");
        }
    }
}
