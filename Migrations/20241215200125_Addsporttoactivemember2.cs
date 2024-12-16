using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GudumholmIdærtAPI.Migrations
{
    /// <inheritdoc />
    public partial class Addsporttoactivemember2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SportsId",
                table: "Sports",
                newName: "SportId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SportId",
                table: "Sports",
                newName: "SportsId");
        }
    }
}
