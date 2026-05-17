using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DocumentGenerator.Context.Migrations
{
    /// <inheritdoc />
    public partial class FixDecimalTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Cost",
                table: "Products",
                type: "decimal(38,15)",
                precision: 38,
                scale: 15,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Cost",
                table: "DocumentProducts",
                type: "decimal(38,15)",
                precision: 38,
                scale: 15,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Cost",
                table: "Products",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(38,15)",
                oldPrecision: 38,
                oldScale: 15);

            migrationBuilder.AlterColumn<decimal>(
                name: "Cost",
                table: "DocumentProducts",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(38,15)",
                oldPrecision: 38,
                oldScale: 15);
        }
    }
}
