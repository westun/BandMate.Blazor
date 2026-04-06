using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BandMate.Domain.Migrations
{
    /// <inheritdoc />
    public partial class AddExternalAlbumIDColumnToSongTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ExternalAlbumID",
                table: "Song",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExternalAlbumID",
                table: "Song");
        }
    }
}
