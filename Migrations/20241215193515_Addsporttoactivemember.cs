using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GudumholmIdærtAPI.Migrations
{
    /// <inheritdoc />
    public partial class Addsporttoactivemember : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActiveMember_SportsName",
                table: "Members");

            migrationBuilder.RenameColumn(
                name: "SportsId",
                table: "Sports",
                newName: "SportsId");

            migrationBuilder.AddColumn<int>(
                name: "ActiveMemberId",
                table: "Sports",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Sports_ActiveMemberId",
                table: "Sports",
                column: "ActiveMemberId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sports_Members_ActiveMemberId",
                table: "Sports",
                column: "ActiveMemberId",
                principalTable: "Members",
                principalColumn: "MemberId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sports_Members_ActiveMemberId",
                table: "Sports");

            migrationBuilder.DropIndex(
                name: "IX_Sports_ActiveMemberId",
                table: "Sports");

            migrationBuilder.DropColumn(
                name: "ActiveMemberId",
                table: "Sports");

            migrationBuilder.RenameColumn(
                name: "SportsId",
                table: "Sports",
                newName: "SportsId");

            migrationBuilder.AddColumn<string>(
                name: "ActiveMember_SportsName",
                table: "Members",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
