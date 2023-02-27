using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MovieCharactersAPI.Migrations
{
    /// <inheritdoc />
    public partial class Seed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Characters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Alias = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Picture = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Characters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Franchises",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Franchises", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MovieTitle = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Genre = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    ReleaseYear = table.Column<int>(type: "int", nullable: false),
                    Director = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Picture = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Trailer = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FranchiseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Movies_Franchises_FranchiseId",
                        column: x => x.FranchiseId,
                        principalTable: "Franchises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CharacterMovie",
                columns: table => new
                {
                    CharactersId = table.Column<int>(type: "int", nullable: false),
                    MoviesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterMovie", x => new { x.CharactersId, x.MoviesId });
                    table.ForeignKey(
                        name: "FK_CharacterMovie_Characters_MoviesId",
                        column: x => x.MoviesId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CharacterMovie_Movies_CharactersId",
                        column: x => x.CharactersId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Characters",
                columns: new[] { "Id", "Alias", "Gender", "Name", "Picture" },
                values: new object[,]
                {
                    { 1, "", "Male", "Leonardo Di Caprio", "Google.com" },
                    { 2, "", "Male", "Robert De Niro", "Google.com" },
                    { 3, "", "Female", "Sandra Bullock", "Google.com" },
                    { 4, "", "Male", "Adam Sandler", "Google.com" }
                });

            migrationBuilder.InsertData(
                table: "Franchises",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Disney", "Disney" },
                    { 2, "MCU", "MCU" }
                });

            migrationBuilder.InsertData(
                table: "CharacterMovie",
                columns: new[] { "CharactersId", "MoviesId" },
                values: new object[] { 4, 3 });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "Director", "FranchiseId", "Genre", "MovieTitle", "Picture", "ReleaseYear", "Trailer" },
                values: new object[,]
                {
                    { 1, "Adam Sandler", 1, "Comedy", "Happy Gilmore", "google.com", 1932, "Youtube.com" },
                    { 2, "??", 1, "Thriller", "Inception", "google.com", 1932, "Youtube.com" },
                    { 3, "James Beanie", 2, "Action", "The Irishman", "google.com", 1932, "Youtube.com" }
                });

            migrationBuilder.InsertData(
                table: "CharacterMovie",
                columns: new[] { "CharactersId", "MoviesId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 2, 2 },
                    { 3, 1 },
                    { 3, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CharacterMovie_MoviesId",
                table: "CharacterMovie",
                column: "MoviesId");

            migrationBuilder.CreateIndex(
                name: "IX_Movies_FranchiseId",
                table: "Movies",
                column: "FranchiseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CharacterMovie");

            migrationBuilder.DropTable(
                name: "Characters");

            migrationBuilder.DropTable(
                name: "Movies");

            migrationBuilder.DropTable(
                name: "Franchises");
        }
    }
}
