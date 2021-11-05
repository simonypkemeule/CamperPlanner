using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CamperPlanner.Migrations
{
    public partial class ContractEndDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDatum",
                table: "Contracten",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)");

            migrationBuilder.AddColumn<DateTime>(
                name: "EindDatum",
                table: "Contracten",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EindDatum",
                table: "Contracten");

            migrationBuilder.AlterColumn<string>(
                name: "StartDatum",
                table: "Contracten",
                type: "varchar(50)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }
    }
}
