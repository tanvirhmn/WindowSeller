using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockModule.DAL.Migrations
{
    public partial class externalMovementDocumentTypeToDocumentName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DocumentType",
                table: "ExternalMovementsArchive",
                newName: "DocumentName");

            migrationBuilder.RenameColumn(
                name: "DocumentType",
                table: "ExternalMovements",
                newName: "DocumentName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DocumentName",
                table: "ExternalMovementsArchive",
                newName: "DocumentType");

            migrationBuilder.RenameColumn(
                name: "DocumentName",
                table: "ExternalMovements",
                newName: "DocumentType");
        }
    }
}
