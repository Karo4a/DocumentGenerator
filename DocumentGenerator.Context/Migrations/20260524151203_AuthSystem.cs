using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DocumentGenerator.Context.Migrations
{
    /// <inheritdoc />
    public partial class AuthSystem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SecurityStamp",
                table: "Users",
                type: "uniqueidentifier",
                nullable: false);

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Expires = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DeletedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("213064b1-4ee0-40ea-bacf-d60dd358fedc"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTimeOffset(new DateTime(2026, 5, 24, 15, 12, 2, 951, DateTimeKind.Unspecified).AddTicks(6471), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 5, 24, 15, 12, 2, 951, DateTimeKind.Unspecified).AddTicks(6472), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("59d20b7b-420d-4dba-b8b5-be625764be5b"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTimeOffset(new DateTime(2026, 5, 24, 15, 12, 2, 951, DateTimeKind.Unspecified).AddTicks(6451), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 5, 24, 15, 12, 2, 951, DateTimeKind.Unspecified).AddTicks(6454), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("9af703f7-8fd7-49a7-b87d-a2c4215cfdb5"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTimeOffset(new DateTime(2026, 5, 24, 15, 12, 2, 951, DateTimeKind.Unspecified).AddTicks(6519), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 5, 24, 15, 12, 2, 951, DateTimeKind.Unspecified).AddTicks(6520), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "Email", "Login", "PasswordHash", "PasswordSalt", "SecurityStamp", "UpdatedAt", "UserRoleId" },
                values: new object[] { new Guid("8abf3cb5-eccd-4799-89d2-3927b19e2f43"), new DateTimeOffset(new DateTime(2026, 5, 24, 15, 12, 3, 180, DateTimeKind.Unspecified).AddTicks(6620), new TimeSpan(0, 0, 0, 0, 0)), null, "", "admin", "7aAiD3rh2J6gFQ2WM4emvTr4vf6UHgwXB2SjM4cok9Y=", "LWhMP2F1cW2mC7D5SJI18+YFUKDSzoBLWnhOlNWEdGE=", new Guid("9756a2bc-0c7d-47c2-bd9c-e4f5e853a8fc"), new DateTimeOffset(new DateTime(2026, 5, 24, 15, 12, 3, 180, DateTimeKind.Unspecified).AddTicks(6636), new TimeSpan(0, 0, 0, 0, 0)), new Guid("9af703f7-8fd7-49a7-b87d-a2c4215cfdb5") });

            migrationBuilder.CreateIndex(
                name: "IX_RefreshToken_UserId",
                table: "RefreshTokens",
                column: "UserId",
                unique: true,
                filter: "\"DeletedAt\" IS NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("8abf3cb5-eccd-4799-89d2-3927b19e2f43"));

            migrationBuilder.DropColumn(
                name: "SecurityStamp",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("213064b1-4ee0-40ea-bacf-d60dd358fedc"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTimeOffset(new DateTime(2026, 5, 18, 18, 5, 58, 14, DateTimeKind.Unspecified).AddTicks(4529), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 5, 18, 18, 5, 58, 14, DateTimeKind.Unspecified).AddTicks(4529), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("59d20b7b-420d-4dba-b8b5-be625764be5b"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTimeOffset(new DateTime(2026, 5, 18, 18, 5, 58, 14, DateTimeKind.Unspecified).AddTicks(4509), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 5, 18, 18, 5, 58, 14, DateTimeKind.Unspecified).AddTicks(4521), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("9af703f7-8fd7-49a7-b87d-a2c4215cfdb5"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTimeOffset(new DateTime(2026, 5, 18, 18, 5, 58, 14, DateTimeKind.Unspecified).AddTicks(4532), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 5, 18, 18, 5, 58, 14, DateTimeKind.Unspecified).AddTicks(4533), new TimeSpan(0, 0, 0, 0, 0)) });
        }
    }
}
