using Microsoft.EntityFrameworkCore.Migrations;

namespace CamperPlanner.Data.Migrations
{
    public partial class VoertuigenStroomaansluiting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Stroomaansluiting",
                table: "Voertuigen",
                type: "char(1)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Stroomaansluiting",
                table: "Voertuigen");
        }
    }
}
