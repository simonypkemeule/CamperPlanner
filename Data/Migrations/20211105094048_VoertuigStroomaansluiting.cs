using Microsoft.EntityFrameworkCore.Migrations;

namespace CamperPlanner.Migrations
{
    public partial class VoertuigStroomaansluiting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Stroomaansluiting",
                table: "Voertuigen",
                type: "varchar(4)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "char(1)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Stroomaansluiting",
                table: "Voertuigen",
                type: "char(1)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(4)",
                oldNullable: true);
        }
    }
}
