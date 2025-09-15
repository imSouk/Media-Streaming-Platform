using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MediaStreamingPlatform_API.Migrations
{
    /// <inheritdoc />
    public partial class CreatePlaylisttable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MediaPlaylistId",
                table: "MediaFiles",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Playlists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UploadedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Playlists", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MediaFiles_MediaPlaylistId",
                table: "MediaFiles",
                column: "MediaPlaylistId");

            migrationBuilder.AddForeignKey(
                name: "FK_MediaFiles_Playlists_MediaPlaylistId",
                table: "MediaFiles",
                column: "MediaPlaylistId",
                principalTable: "Playlists",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MediaFiles_Playlists_MediaPlaylistId",
                table: "MediaFiles");

            migrationBuilder.DropTable(
                name: "Playlists");

            migrationBuilder.DropIndex(
                name: "IX_MediaFiles_MediaPlaylistId",
                table: "MediaFiles");

            migrationBuilder.DropColumn(
                name: "MediaPlaylistId",
                table: "MediaFiles");
        }
    }
}
