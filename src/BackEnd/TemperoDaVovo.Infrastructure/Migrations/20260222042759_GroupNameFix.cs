using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TemperoDaVovo.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class GroupNameFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GroupName",
                table: "OrderItemSideDishes",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GroupName",
                table: "OrderItemSideDishes");
        }
    }
}
