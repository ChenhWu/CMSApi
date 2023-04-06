using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CMS_miniAPI.Migrations
{
    /// <inheritdoc />
    public partial class carPosition : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "PosX",
                type: "float",
                table: "Beacons",
                nullable: true
            );
            migrationBuilder.AddColumn<double>(
                name: "PosY",
                type: "float",
                table: "Beacons",
                nullable: true
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PosX",
                table: "Beacons"
            );
            migrationBuilder.DropColumn(
                name: "PosY",
                table: "Beacons"
            );
        }
    }
}
