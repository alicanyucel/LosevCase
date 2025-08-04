using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Losev.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class mg232347 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PaswordHash",
                table: "Users",
                newName: "Password");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Users",
                newName: "PaswordHash");
        }
    }
}
