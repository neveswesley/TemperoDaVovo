using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TemperoDaVovo.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SideDishGroupByProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsRequired",
                table: "SideDishesGroups",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "ProductSideDishGroups",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<bool>(
                name: "IsRequired",
                table: "ProductSideDishGroups",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_ProductSideDishGroups_SideDishGroupId",
                table: "ProductSideDishGroups",
                column: "SideDishGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSideDishGroups_Products_ProductId",
                table: "ProductSideDishGroups",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSideDishGroups_SideDishesGroups_SideDishGroupId",
                table: "ProductSideDishGroups",
                column: "SideDishGroupId",
                principalTable: "SideDishesGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductSideDishGroups_Products_ProductId",
                table: "ProductSideDishGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductSideDishGroups_SideDishesGroups_SideDishGroupId",
                table: "ProductSideDishGroups");

            migrationBuilder.DropIndex(
                name: "IX_ProductSideDishGroups_SideDishGroupId",
                table: "ProductSideDishGroups");

            migrationBuilder.DropColumn(
                name: "IsRequired",
                table: "SideDishesGroups");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ProductSideDishGroups");

            migrationBuilder.DropColumn(
                name: "IsRequired",
                table: "ProductSideDishGroups");
        }
    }
}
