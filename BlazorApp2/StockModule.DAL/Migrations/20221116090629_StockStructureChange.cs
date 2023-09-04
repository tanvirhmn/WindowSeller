using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockModule.DAL.Migrations
{
    public partial class StockStructureChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockMovemenet_Stock_StockId",
                table: "StockMovemenet");

            migrationBuilder.DropIndex(
                name: "IX_StockMovemenet_StockId",
                table: "StockMovemenet");

            migrationBuilder.DropColumn(
                name: "EntryWarehouseId",
                table: "StockMovemenet");

            migrationBuilder.DropColumn(
                name: "StockId",
                table: "StockMovemenet");

            migrationBuilder.AddColumn<int>(
                name: "FromStockId",
                table: "StockMovemenet",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ToStockId",
                table: "StockMovemenet",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StockMovemenet_FromStockId",
                table: "StockMovemenet",
                column: "FromStockId");

            migrationBuilder.CreateIndex(
                name: "IX_StockMovemenet_ToStockId",
                table: "StockMovemenet",
                column: "ToStockId");

            migrationBuilder.AddForeignKey(
                name: "FK_StockMovemenet_Stock_FromStockId",
                table: "StockMovemenet",
                column: "FromStockId",
                principalTable: "Stock",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StockMovemenet_Stock_ToStockId",
                table: "StockMovemenet",
                column: "ToStockId",
                principalTable: "Stock",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockMovemenet_Stock_FromStockId",
                table: "StockMovemenet");

            migrationBuilder.DropForeignKey(
                name: "FK_StockMovemenet_Stock_ToStockId",
                table: "StockMovemenet");

            migrationBuilder.DropIndex(
                name: "IX_StockMovemenet_FromStockId",
                table: "StockMovemenet");

            migrationBuilder.DropIndex(
                name: "IX_StockMovemenet_ToStockId",
                table: "StockMovemenet");

            migrationBuilder.DropColumn(
                name: "FromStockId",
                table: "StockMovemenet");

            migrationBuilder.DropColumn(
                name: "ToStockId",
                table: "StockMovemenet");

            migrationBuilder.AddColumn<int>(
                name: "EntryWarehouseId",
                table: "StockMovemenet",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StockId",
                table: "StockMovemenet",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_StockMovemenet_StockId",
                table: "StockMovemenet",
                column: "StockId");

            migrationBuilder.AddForeignKey(
                name: "FK_StockMovemenet_Stock_StockId",
                table: "StockMovemenet",
                column: "StockId",
                principalTable: "Stock",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
