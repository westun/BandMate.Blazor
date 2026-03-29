using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BandMate.Blazor.Migrations
{
    /// <inheritdoc />
    public partial class AddAccountIDToAspNetUsersTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AccountID",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountID",
                table: "AspNetUsers");
        }
    }
}
