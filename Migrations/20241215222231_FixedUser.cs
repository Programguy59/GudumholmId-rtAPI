using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GudumholmIdærtAPI.Migrations
{
    /// <inheritdoc />
    public partial class FixedUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "ActiveMemberSports",
                columns: table => new
                {
                    ActiveMemberId = table.Column<int>(type: "int", nullable: false),
                    SportId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActiveMemberSports", x => new { x.ActiveMemberId, x.SportId });
                    table.ForeignKey(
                        name: "FK_ActiveMemberSports_Members_ActiveMemberId",
                        column: x => x.ActiveMemberId,
                        principalTable: "Members",
                        principalColumn: "MemberId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActiveMemberSports_Sports_SportId",
                        column: x => x.SportId,
                        principalTable: "Sports",
                        principalColumn: "SportId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActiveMemberSports_SportId",
                table: "ActiveMemberSports",
                column: "SportId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActiveMemberSports");

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
    }
}
