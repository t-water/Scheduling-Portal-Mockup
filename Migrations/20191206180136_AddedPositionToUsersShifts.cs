using Microsoft.EntityFrameworkCore.Migrations;

namespace TEServerTest.Migrations
{
    public partial class AddedPositionToUsersShifts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PositionID",
                table: "UsersShifts",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsersShifts_PositionID",
                table: "UsersShifts",
                column: "PositionID");

            migrationBuilder.AddForeignKey(
                name: "FK_UsersShifts_AspNetRoles_PositionID",
                table: "UsersShifts",
                column: "PositionID",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsersShifts_AspNetRoles_PositionID",
                table: "UsersShifts");

            migrationBuilder.DropIndex(
                name: "IX_UsersShifts_PositionID",
                table: "UsersShifts");

            migrationBuilder.DropColumn(
                name: "PositionID",
                table: "UsersShifts");
        }
    }
}
