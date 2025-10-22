using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OmniCarPark.Migrations
{
    /// <inheritdoc />
    public partial class add_vehicle_type_to_entry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VehicleType",
                table: "ParkEntries",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VehicleType",
                table: "ParkEntries");
        }
    }
}
