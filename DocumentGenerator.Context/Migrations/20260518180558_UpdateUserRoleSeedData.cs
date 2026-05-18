using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DocumentGenerator.Context.Migrations;

/// <inheritdoc />
public partial class UpdateUserRoleSeedData : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DeleteData(
            table: "Roles",
            keyColumn: "Id",
            keyValue: new Guid("0558e0ec-5258-48a6-aa61-ff5f44dd84a1"));

        migrationBuilder.DeleteData(
            table: "Roles",
            keyColumn: "Id",
            keyValue: new Guid("36678a59-a9f2-4222-abb0-588da8eadcda"));

        migrationBuilder.DeleteData(
            table: "Roles",
            keyColumn: "Id",
            keyValue: new Guid("6e2f628c-5dfc-49c0-ba5d-c2142625f0b1"));

        migrationBuilder.InsertData(
            table: "Roles",
            columns: new[] { "Id", "CreatedAt", "DeletedAt", "Role", "UpdatedAt" },
            values: new object[,]
            {
                { new Guid("213064b1-4ee0-40ea-bacf-d60dd358fedc"), new DateTimeOffset(new DateTime(2026, 5, 18, 18, 5, 58, 14, DateTimeKind.Unspecified).AddTicks(4529), new TimeSpan(0, 0, 0, 0, 0)), null, "Editor", new DateTimeOffset(new DateTime(2026, 5, 18, 18, 5, 58, 14, DateTimeKind.Unspecified).AddTicks(4529), new TimeSpan(0, 0, 0, 0, 0)) },
                { new Guid("59d20b7b-420d-4dba-b8b5-be625764be5b"), new DateTimeOffset(new DateTime(2026, 5, 18, 18, 5, 58, 14, DateTimeKind.Unspecified).AddTicks(4509), new TimeSpan(0, 0, 0, 0, 0)), null, "Viewer", new DateTimeOffset(new DateTime(2026, 5, 18, 18, 5, 58, 14, DateTimeKind.Unspecified).AddTicks(4521), new TimeSpan(0, 0, 0, 0, 0)) },
                { new Guid("9af703f7-8fd7-49a7-b87d-a2c4215cfdb5"), new DateTimeOffset(new DateTime(2026, 5, 18, 18, 5, 58, 14, DateTimeKind.Unspecified).AddTicks(4532), new TimeSpan(0, 0, 0, 0, 0)), null, "Admin", new DateTimeOffset(new DateTime(2026, 5, 18, 18, 5, 58, 14, DateTimeKind.Unspecified).AddTicks(4533), new TimeSpan(0, 0, 0, 0, 0)) }
            });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DeleteData(
            table: "Roles",
            keyColumn: "Id",
            keyValue: new Guid("213064b1-4ee0-40ea-bacf-d60dd358fedc"));

        migrationBuilder.DeleteData(
            table: "Roles",
            keyColumn: "Id",
            keyValue: new Guid("59d20b7b-420d-4dba-b8b5-be625764be5b"));

        migrationBuilder.DeleteData(
            table: "Roles",
            keyColumn: "Id",
            keyValue: new Guid("9af703f7-8fd7-49a7-b87d-a2c4215cfdb5"));

        migrationBuilder.InsertData(
            table: "Roles",
            columns: new[] { "Id", "CreatedAt", "DeletedAt", "Role", "UpdatedAt" },
            values: new object[,]
            {
                { new Guid("0558e0ec-5258-48a6-aa61-ff5f44dd84a1"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "Editor", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                { new Guid("36678a59-a9f2-4222-abb0-588da8eadcda"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "Admin", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                { new Guid("6e2f628c-5dfc-49c0-ba5d-c2142625f0b1"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "Viewer", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) }
            });
    }
}
