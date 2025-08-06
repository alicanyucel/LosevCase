using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Losev.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class mg88989 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PassName",
                table: "Groups",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Groups",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Groups",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PassName",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "Url",
                table: "Groups");
        }
    }
}
