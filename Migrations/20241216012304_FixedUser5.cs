using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GudumholmIdærtAPI.Migrations
{
    /// <inheritdoc />
    public partial class FixedUser5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SportsName",
                table: "Members");

            migrationBuilder.AddColumn<int>(
                name: "SportId",
                table: "Members",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Members_SportId",
                table: "Members",
                column: "SportId");

            migrationBuilder.AddForeignKey(
                name: "FK_Members_Sports_SportId",
                table: "Members",
                column: "SportId",
                principalTable: "Sports",
                principalColumn: "SportId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Members_Sports_SportId",
                table: "Members");

            migrationBuilder.DropIndex(
                name: "IX_Members_SportId",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "SportId",
                table: "Members");

            migrationBuilder.AddColumn<string>(
                name: "SportsName",
                table: "Members",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
