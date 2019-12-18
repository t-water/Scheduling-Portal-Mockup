using Microsoft.EntityFrameworkCore.Migrations;

namespace TEServerTest.Migrations
{
    public partial class ChangedStatusColumnNameShiftSwaps : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "ShiftSwaps");

            migrationBuilder.AddColumn<int>(
                name: "RequestStatus",
                table: "ShiftSwaps",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RequestStatus",
                table: "ShiftSwaps");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "ShiftSwaps",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
