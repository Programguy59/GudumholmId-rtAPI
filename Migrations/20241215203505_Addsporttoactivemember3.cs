using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GudumholmIdærtAPI.Migrations
{
    /// <inheritdoc />
    public partial class Addsporttoactivemember3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "House",
                table: "Members");

            migrationBuilder.RenameColumn(
                name: "HouseMembers",
                table: "Houses",
                newName: "Address");

            migrationBuilder.AddColumn<int>(
                name: "HouseId",
                table: "Members",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Members_HouseId",
                table: "Members",
                column: "HouseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Members_Houses_HouseId",
                table: "Members",
                column: "HouseId",
                principalTable: "Houses",
                principalColumn: "HouseId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Members_Houses_HouseId",
                table: "Members");

            migrationBuilder.DropIndex(
                name: "IX_Members_HouseId",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "HouseId",
                table: "Members");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "Houses",
                newName: "HouseMembers");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Members",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "House",
                table: "Members",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
