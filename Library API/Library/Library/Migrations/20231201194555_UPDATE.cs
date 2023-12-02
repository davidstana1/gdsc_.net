using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library.Migrations
{
    /// <inheritdoc />
    public partial class UPDATE : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GenreId",
                table: "BooksEnumerable",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    GenreName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BooksEnumerable_GenreId",
                table: "BooksEnumerable",
                column: "GenreId");

            migrationBuilder.AddForeignKey(
                name: "FK_BooksEnumerable_Genres_GenreId",
                table: "BooksEnumerable",
                column: "GenreId",
                principalTable: "Genres",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BooksEnumerable_Genres_GenreId",
                table: "BooksEnumerable");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropIndex(
                name: "IX_BooksEnumerable_GenreId",
                table: "BooksEnumerable");

            migrationBuilder.DropColumn(
                name: "GenreId",
                table: "BooksEnumerable");
        }
    }
}
