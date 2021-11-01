using Microsoft.EntityFrameworkCore.Migrations;

namespace CamperPlanner.Data.Migrations
{
    public partial class ForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Voertuigen",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Voertuigen",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Voertuigen_ApplicationUserId",
                table: "Voertuigen",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Voertuigen_AspNetUsers_ApplicationUserId",
                table: "Voertuigen",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Voertuigen_AspNetUsers_ApplicationUserId",
                table: "Voertuigen");

            migrationBuilder.DropIndex(
                name: "IX_Voertuigen_ApplicationUserId",
                table: "Voertuigen");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Voertuigen");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Voertuigen");
        }
    }
}
