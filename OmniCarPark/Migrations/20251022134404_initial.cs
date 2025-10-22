using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OmniCarPark.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ParkingSpaces",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParkingSpaces", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ParkEntries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RegistrationPlate = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    ParkingSpaceId = table.Column<int>(type: "integer", nullable: false),
                    ParkingEntryDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ParkingExitDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParkEntries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ParkEntries_ParkingSpaces_ParkingSpaceId",
                        column: x => x.ParkingSpaceId,
                        principalTable: "ParkingSpaces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ParkEntries_ParkingSpaceId",
                table: "ParkEntries",
                column: "ParkingSpaceId",
                filter: "\"ParkingExitDate\" IS NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ParkEntries_RegistrationPlate",
                table: "ParkEntries",
                column: "RegistrationPlate",
                filter: "\"ParkingExitDate\" IS NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ParkEntries");

            migrationBuilder.DropTable(
                name: "ParkingSpaces");
        }
    }
}
