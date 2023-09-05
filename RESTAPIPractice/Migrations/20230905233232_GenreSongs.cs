using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RESTAPIPractice.Migrations
{
    public partial class GenreSongs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "GenreSong",
                columns: new[] { "GenresId", "SongsId" },
                values: new object[,]
                {
                    { 123, 234 },
                    { 124, 236 },
                    { 125, 235 },
                    { 125, 237 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "GenreSong",
                keyColumns: new[] { "GenresId", "SongsId" },
                keyValues: new object[] { 123, 234 });

            migrationBuilder.DeleteData(
                table: "GenreSong",
                keyColumns: new[] { "GenresId", "SongsId" },
                keyValues: new object[] { 124, 236 });

            migrationBuilder.DeleteData(
                table: "GenreSong",
                keyColumns: new[] { "GenresId", "SongsId" },
                keyValues: new object[] { 125, 235 });

            migrationBuilder.DeleteData(
                table: "GenreSong",
                keyColumns: new[] { "GenresId", "SongsId" },
                keyValues: new object[] { 125, 237 });
        }
    }
}
