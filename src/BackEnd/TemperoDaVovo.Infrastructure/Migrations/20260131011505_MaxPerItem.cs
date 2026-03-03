using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TemperoDaVovo.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MaxPerItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxPerItem",
                table: "SideDishes");

            migrationBuilder.AlterColumn<decimal>(
                name: "UnitPrice",
                table: "SideDishes",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "TEXT",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "UnitPrice",
                table: "SideDishes",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "TEXT");

            migrationBuilder.AddColumn<int>(
                name: "MaxPerItem",
                table: "SideDishes",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
