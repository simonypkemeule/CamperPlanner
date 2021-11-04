using Microsoft.EntityFrameworkCore.Migrations;

namespace CamperPlanner.Data.Migrations
{
    public partial class ForeignKeyUserId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Voertuigen",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Voertuigen_UserId",
                table: "Voertuigen",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Voertuigen_AspNetUsers_UserId",
                table: "Voertuigen",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Voertuigen_AspNetUsers_UserId",
                table: "Voertuigen");

            migrationBuilder.DropIndex(
                name: "IX_Voertuigen_UserId",
                table: "Voertuigen");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Voertuigen",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Voertuigen",
                type: "nvarchar(450)",
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
    }
}
