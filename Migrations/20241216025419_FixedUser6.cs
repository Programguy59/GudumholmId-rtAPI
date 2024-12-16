using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GudumholmIdærtAPI.Migrations
{
    /// <inheritdoc />
    public partial class FixedUser6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AmountOfChildren",
                table: "Members");

            migrationBuilder.AddColumn<int>(
                name: "ParentMemberMemberId",
                table: "Members",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Members_ParentMemberMemberId",
                table: "Members",
                column: "ParentMemberMemberId");

            migrationBuilder.AddForeignKey(
                name: "FK_Members_Members_ParentMemberMemberId",
                table: "Members",
                column: "ParentMemberMemberId",
                principalTable: "Members",
                principalColumn: "MemberId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Members_Members_ParentMemberMemberId",
                table: "Members");

            migrationBuilder.DropIndex(
                name: "IX_Members_ParentMemberMemberId",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "ParentMemberMemberId",
                table: "Members");

            migrationBuilder.AddColumn<string>(
                name: "AmountOfChildren",
                table: "Members",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
