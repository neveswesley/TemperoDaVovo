using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TemperoDaVovo.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Neighborhoods : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Neighborhoods",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RestaurantId = table.Column<int>(nullable: false),
                    // outras colunas aqui
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Neighborhoods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Neighborhoods_Restaurant_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "Restaurant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Neighborhoods_Restaurant_RestaurantId",
                table: "Neighborhoods");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Neighborhoods",
                table: "Neighborhoods");

            migrationBuilder.RenameTable(
                name: "Neighborhoods",
                newName: "Neighborhood");

            migrationBuilder.RenameIndex(
                name: "IX_Neighborhoods_RestaurantId",
                table: "Neighborhood",
                newName: "IX_Neighborhood_RestaurantId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Neighborhood",
                table: "Neighborhood",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Neighborhood_Restaurant_RestaurantId",
                table: "Neighborhood",
                column: "RestaurantId",
                principalTable: "Restaurant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
