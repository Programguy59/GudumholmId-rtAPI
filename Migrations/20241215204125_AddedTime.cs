using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GudumholmIdærtAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddedTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeSincePassive",
                table: "Members");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateBecamePassive",
                table: "Members",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateBecamePassive",
                table: "Members");

            migrationBuilder.AddColumn<string>(
                name: "TimeSincePassive",
                table: "Members",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
