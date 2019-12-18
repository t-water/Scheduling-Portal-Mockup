using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TEServerTest.Migrations
{
    public partial class AddedEndDateToTimeOff : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberOfDays",
                table: "TimeOff");

            migrationBuilder.AddColumn<DateTime>(
                name: "End",
                table: "TimeOff",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "End",
                table: "TimeOff");

            migrationBuilder.AddColumn<int>(
                name: "NumberOfDays",
                table: "TimeOff",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
