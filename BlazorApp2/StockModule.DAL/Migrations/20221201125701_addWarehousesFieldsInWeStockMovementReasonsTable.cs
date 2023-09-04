using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockModule.DAL.Migrations
{
    public partial class addWarehousesFieldsInWeStockMovementReasonsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FromWarehouseId",
                table: "StockMovementReasons",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ToWarehouseId",
                table: "StockMovementReasons",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StockMovementReasons_FromWarehouseId",
                table: "StockMovementReasons",
                column: "FromWarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_StockMovementReasons_ToWarehouseId",
                table: "StockMovementReasons",
                column: "ToWarehouseId");

            migrationBuilder.AddForeignKey(
                name: "FK_StockMovementReasons_Warehouses_FromWarehouseId",
                table: "StockMovementReasons",
                column: "FromWarehouseId",
                principalTable: "Warehouses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StockMovementReasons_Warehouses_ToWarehouseId",
                table: "StockMovementReasons",
                column: "ToWarehouseId",
                principalTable: "Warehouses",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockMovementReasons_Warehouses_FromWarehouseId",
                table: "StockMovementReasons");

            migrationBuilder.DropForeignKey(
                name: "FK_StockMovementReasons_Warehouses_ToWarehouseId",
                table: "StockMovementReasons");

            migrationBuilder.DropIndex(
                name: "IX_StockMovementReasons_FromWarehouseId",
                table: "StockMovementReasons");

            migrationBuilder.DropIndex(
                name: "IX_StockMovementReasons_ToWarehouseId",
                table: "StockMovementReasons");

            migrationBuilder.DropColumn(
                name: "FromWarehouseId",
                table: "StockMovementReasons");

            migrationBuilder.DropColumn(
                name: "ToWarehouseId",
                table: "StockMovementReasons");
        }
    }
}
