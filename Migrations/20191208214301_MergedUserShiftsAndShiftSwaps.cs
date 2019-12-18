using Microsoft.EntityFrameworkCore.Migrations;

namespace TEServerTest.Migrations
{
    public partial class MergedUserShiftsAndShiftSwaps : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShiftSwaps");

            migrationBuilder.AddColumn<int>(
                name: "RequestStatus",
                table: "UsersShifts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "TakenByID",
                table: "UsersShifts",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsersShifts_TakenByID",
                table: "UsersShifts",
                column: "TakenByID");

            migrationBuilder.AddForeignKey(
                name: "FK_UsersShifts_AspNetUsers_TakenByID",
                table: "UsersShifts",
                column: "TakenByID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsersShifts_AspNetUsers_TakenByID",
                table: "UsersShifts");

            migrationBuilder.DropIndex(
                name: "IX_UsersShifts_TakenByID",
                table: "UsersShifts");

            migrationBuilder.DropColumn(
                name: "RequestStatus",
                table: "UsersShifts");

            migrationBuilder.DropColumn(
                name: "TakenByID",
                table: "UsersShifts");

            migrationBuilder.CreateTable(
                name: "ShiftSwaps",
                columns: table => new
                {
                    ShiftSwapID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestStatus = table.Column<int>(type: "int", nullable: false),
                    TakenByID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UserShiftID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShiftSwaps", x => x.ShiftSwapID);
                    table.ForeignKey(
                        name: "FK_ShiftSwaps_AspNetUsers_TakenByID",
                        column: x => x.TakenByID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ShiftSwaps_UsersShifts_UserShiftID",
                        column: x => x.UserShiftID,
                        principalTable: "UsersShifts",
                        principalColumn: "UserShiftID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShiftSwaps_TakenByID",
                table: "ShiftSwaps",
                column: "TakenByID");

            migrationBuilder.CreateIndex(
                name: "IX_ShiftSwaps_UserShiftID",
                table: "ShiftSwaps",
                column: "UserShiftID");
        }
    }
}
