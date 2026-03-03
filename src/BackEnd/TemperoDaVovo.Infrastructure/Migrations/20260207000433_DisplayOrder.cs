using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TemperoDaVovo.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DisplayOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductSideDishGroups",
                table: "ProductSideDishGroups");

            migrationBuilder.DropIndex(
                name: "IX_ProductSideDishGroups_ProductId_SideDishGroupId",
                table: "ProductSideDishGroups");

            migrationBuilder.RenameColumn(
                name: "Order",
                table: "Categories",
                newName: "DisplayOrder");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductSideDishGroups",
                table: "ProductSideDishGroups",
                columns: new[] { "ProductId", "SideDishGroupId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductSideDishGroups",
                table: "ProductSideDishGroups");

            migrationBuilder.RenameColumn(
                name: "DisplayOrder",
                table: "Categories",
                newName: "Order");

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
    }
}
