using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TemperoDaVovo.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CategoryRepositoryUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductSideDishGroups",
                table: "ProductSideDishGroups");

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "Categories",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductSideDishGroups",
                table: "ProductSideDishGroups",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSideDishGroups_ProductId_SideDishGroupId",
                table: "ProductSideDishGroups",
                columns: new[] { "ProductId", "SideDishGroupId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductSideDishGroups",
                table: "ProductSideDishGroups");

            migrationBuilder.DropIndex(
                name: "IX_ProductSideDishGroups_ProductId_SideDishGroupId",
                table: "ProductSideDishGroups");

            migrationBuilder.DropColumn(
                name: "Order",
                table: "Categories");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductSideDishGroups",
                table: "ProductSideDishGroups",
                columns: new[] { "ProductId", "SideDishGroupId" });
        }
    }
}
