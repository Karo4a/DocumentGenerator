using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DocumentGenerator.Context.Migrations
{
    /// <inheritdoc />
    public partial class ReviewChanges1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameIndex(
                name: "IX_Product_DeletedAt",
                table: "Products",
                newName: "IX_Product_Name");

            migrationBuilder.RenameIndex(
                name: "IX_Party_DeletedAt",
                table: "Parties",
                newName: "IX_Party_TaxId");

            migrationBuilder.CreateIndex(
                name: "IX_Document_DocumentNumber",
                table: "Documents",
                column: "DocumentNumber",
                unique: true,
                filter: "\"DeletedAt\" IS NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Document_DocumentNumber",
                table: "Documents");

            migrationBuilder.RenameIndex(
                name: "IX_Product_Name",
                table: "Products",
                newName: "IX_Product_DeletedAt");

            migrationBuilder.RenameIndex(
                name: "IX_Party_TaxId",
                table: "Parties",
                newName: "IX_Party_DeletedAt");
        }
    }
}
