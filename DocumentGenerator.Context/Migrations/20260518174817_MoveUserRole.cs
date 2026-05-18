using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DocumentGenerator.Context.Migrations;

/// <inheritdoc />
public partial class MoveUserRole : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "Role",
            table: "Users");

        migrationBuilder.AddColumn<Guid>(
            name: "UserRoleId",
            table: "Users",
            type: "uniqueidentifier",
            nullable: false);

        migrationBuilder.AlterColumn<decimal>(
            name: "Cost",
            table: "Products",
            type: "decimal(18,2)",
            precision: 18,
            scale: 2,
            nullable: false,
            oldClrType: typeof(decimal),
            oldType: "decimal(38,15)",
            oldPrecision: 38,
            oldScale: 15);

        migrationBuilder.AlterColumn<decimal>(
            name: "Cost",
            table: "DocumentProducts",
            type: "decimal(18,2)",
            precision: 18,
            scale: 2,
            nullable: false,
            oldClrType: typeof(decimal),
            oldType: "decimal(38,15)",
            oldPrecision: 38,
            oldScale: 15);

        migrationBuilder.CreateTable(
            name: "Roles",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Role = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                DeletedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Roles", x => x.Id);
            });

        migrationBuilder.CreateIndex(
            name: "IX_Users_UserRoleId",
            table: "Users",
            column: "UserRoleId");

        migrationBuilder.AddForeignKey(
            name: "FK_Users_Roles_UserRoleId",
            table: "Users",
            column: "UserRoleId",
            principalTable: "Roles",
            principalColumn: "Id",
            onDelete: ReferentialAction.Restrict);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_Users_Roles_UserRoleId",
            table: "Users");

        migrationBuilder.DropTable(
            name: "Roles");

        migrationBuilder.DropIndex(
            name: "IX_Users_UserRoleId",
            table: "Users");

        migrationBuilder.DropColumn(
            name: "UserRoleId",
            table: "Users");

        migrationBuilder.AddColumn<string>(
            name: "Role",
            table: "Users",
            type: "nvarchar(255)",
            maxLength: 255,
            nullable: false,
            defaultValue: "");

        migrationBuilder.AlterColumn<decimal>(
            name: "Cost",
            table: "Products",
            type: "decimal(38,15)",
            precision: 38,
            scale: 15,
            nullable: false,
            oldClrType: typeof(decimal),
            oldType: "decimal(18,2)",
            oldPrecision: 18,
            oldScale: 2);

        migrationBuilder.AlterColumn<decimal>(
            name: "Cost",
            table: "DocumentProducts",
            type: "decimal(38,15)",
            precision: 38,
            scale: 15,
            nullable: false,
            oldClrType: typeof(decimal),
            oldType: "decimal(18,2)",
            oldPrecision: 18,
            oldScale: 2);
    }
}
