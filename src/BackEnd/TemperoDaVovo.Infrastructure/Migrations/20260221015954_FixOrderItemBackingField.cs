using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TemperoDaVovo.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixOrderItemBackingField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItemSideDishes_OrderItems_OrderItemId",
                table: "OrderItemSideDishes");

            migrationBuilder.DropIndex(
                name: "IX_OrderItemSideDishes_OrderItemId",
                table: "OrderItemSideDishes");

            migrationBuilder.AlterColumn<Guid>(
                name: "OrderItemId",
                table: "OrderItemSideDishes",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderItemSideDishes_OrderItemId_OriginalSideDishId",
                table: "OrderItemSideDishes",
                columns: new[] { "OrderItemId", "OriginalSideDishId" });

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItemSideDishes_OrderItems_OrderItemId",
                table: "OrderItemSideDishes",
                column: "OrderItemId",
                principalTable: "OrderItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItemSideDishes_OrderItems_OrderItemId",
                table: "OrderItemSideDishes");

            migrationBuilder.DropIndex(
                name: "IX_OrderItemSideDishes_OrderItemId_OriginalSideDishId",
                table: "OrderItemSideDishes");

            migrationBuilder.AlterColumn<Guid>(
                name: "OrderItemId",
                table: "OrderItemSideDishes",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItemSideDishes_OrderItemId",
                table: "OrderItemSideDishes",
                column: "OrderItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItemSideDishes_OrderItems_OrderItemId",
                table: "OrderItemSideDishes",
                column: "OrderItemId",
                principalTable: "OrderItems",
                principalColumn: "Id");
        }
    }
}
