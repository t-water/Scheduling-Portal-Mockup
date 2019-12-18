using Microsoft.EntityFrameworkCore.Migrations;

namespace TEServerTest.Migrations
{
    public partial class AddedShiftSwaps : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ShiftSwaps",
                columns: table => new
                {
                    ShiftSwapID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserShiftID = table.Column<int>(nullable: false),
                    TakenByID = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false)
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShiftSwaps");
        }
    }
}
