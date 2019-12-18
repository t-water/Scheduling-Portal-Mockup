using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TEServerTest.Migrations
{
    public partial class AddedTimeOff : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsersShifts_AspNetUsers_UserID",
                table: "UsersShifts");

            migrationBuilder.AlterColumn<string>(
                name: "UserID",
                table: "UsersShifts",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "TimeOff",
                columns: table => new
                {
                    TimeOffID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Start = table.Column<DateTime>(nullable: false),
                    NumberOfDays = table.Column<int>(nullable: false),
                    AdditionalInformation = table.Column<string>(maxLength: 500, nullable: true),
                    UserID = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeOff", x => x.TimeOffID);
                    table.ForeignKey(
                        name: "FK_TimeOff_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TimeOff_UserID",
                table: "TimeOff",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_UsersShifts_AspNetUsers_UserID",
                table: "UsersShifts",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsersShifts_AspNetUsers_UserID",
                table: "UsersShifts");

            migrationBuilder.DropTable(
                name: "TimeOff");

            migrationBuilder.AlterColumn<string>(
                name: "UserID",
                table: "UsersShifts",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddForeignKey(
                name: "FK_UsersShifts_AspNetUsers_UserID",
                table: "UsersShifts",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
