using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CMS_miniAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddBaseCarCreatedType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Type",
                table: "Cars",
                type: "bigint",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Cars");
        }
    }
}
