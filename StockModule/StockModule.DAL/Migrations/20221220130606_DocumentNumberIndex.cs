using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockModule.DAL.Migrations
{
    /// <inheritdoc />
    public partial class DocumentNumberIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_StockMovement_DocumentNumber",
                table: "StockMovement",
                column: "DocumentNumber");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_StockMovement_DocumentNumber",
                table: "StockMovement");
        }
    }
}
