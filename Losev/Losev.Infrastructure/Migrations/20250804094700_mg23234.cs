using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Losev.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class mg23234 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PaswordHash",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaswordHash",
                table: "Users");
        }
    }
}
