using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DocumentGenerator.Context.Migrations
{
    /// <inheritdoc />
    public partial class ChangedIndexForDocumentNumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Document_DocumentNumber",
                table: "Documents");

            migrationBuilder.CreateIndex(
                name: "IX_Document_ContractNumber_DocumentNumber",
                table: "Documents",
                columns: new[] { "ContractNumber", "DocumentNumber" },
                unique: true,
                filter: "\"DeletedAt\" IS NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Document_ContractNumber_DocumentNumber",
                table: "Documents");

            migrationBuilder.CreateIndex(
                name: "IX_Document_DocumentNumber",
                table: "Documents",
                column: "DocumentNumber",
                unique: true,
                filter: "\"DeletedAt\" IS NULL");
        }
    }
}
