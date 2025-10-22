using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OmniCarPark.Migrations
{
    /// <inheritdoc />
    public partial class rename_date_to_datetime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ParkingExitDate",
                table: "ParkEntries",
                newName: "ParkingExitDateTime");

            migrationBuilder.RenameColumn(
                name: "ParkingEntryDate",
                table: "ParkEntries",
                newName: "ParkingEntryDateTime");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ParkingExitDateTime",
                table: "ParkEntries",
                newName: "ParkingExitDate");

            migrationBuilder.RenameColumn(
                name: "ParkingEntryDateTime",
                table: "ParkEntries",
                newName: "ParkingEntryDate");
        }
    }
}
