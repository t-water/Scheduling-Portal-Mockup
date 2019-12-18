using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TEServerTest.Migrations
{
    public partial class AddedUsersShiftsJoin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UsersShifts",
                columns: table => new
                {
                    UserShiftID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<string>(nullable: true),
                    ShiftID = table.Column<int>(nullable: false),
                    UserStart = table.Column<DateTime>(nullable: false),
                    UserEnd = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersShifts", x => x.UserShiftID);
                    table.ForeignKey(
                        name: "FK_UsersShifts_Shifts_ShiftID",
                        column: x => x.ShiftID,
                        principalTable: "Shifts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsersShifts_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UsersShifts_ShiftID",
                table: "UsersShifts",
                column: "ShiftID");

            migrationBuilder.CreateIndex(
                name: "IX_UsersShifts_UserID",
                table: "UsersShifts",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsersShifts");
        }
    }
}
