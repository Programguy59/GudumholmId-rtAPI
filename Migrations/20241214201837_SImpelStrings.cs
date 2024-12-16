using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GudumholmIdærtAPI.Migrations
{
    /// <inheritdoc />
    public partial class SImpelStrings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "House",
                table: "Members",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SportsName",
                table: "Members",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "House",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "SportsName",
                table: "Members");
        }
    }
}
