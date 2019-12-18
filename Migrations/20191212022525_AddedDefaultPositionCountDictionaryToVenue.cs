using Microsoft.EntityFrameworkCore.Migrations;

namespace TEServerTest.Migrations
{
    public partial class AddedDefaultPositionCountDictionaryToVenue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DefaultCommunityRoomLiaisons",
                table: "Venues",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DefaultConcessionsManagers",
                table: "Venues",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DefaultEventUshers",
                table: "Venues",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DefaultExecutiveHouseManagers",
                table: "Venues",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DefaultFloorManagers",
                table: "Venues",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DefaultHouseManagers",
                table: "Venues",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DefaultCommunityRoomLiaisons",
                table: "Venues");

            migrationBuilder.DropColumn(
                name: "DefaultConcessionsManagers",
                table: "Venues");

            migrationBuilder.DropColumn(
                name: "DefaultEventUshers",
                table: "Venues");

            migrationBuilder.DropColumn(
                name: "DefaultExecutiveHouseManagers",
                table: "Venues");

            migrationBuilder.DropColumn(
                name: "DefaultFloorManagers",
                table: "Venues");

            migrationBuilder.DropColumn(
                name: "DefaultHouseManagers",
                table: "Venues");
        }
    }
}
