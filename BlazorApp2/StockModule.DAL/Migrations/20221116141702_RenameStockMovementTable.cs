using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockModule.DAL.Migrations
{
    public partial class RenameStockMovementTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockMovemenet_Employees_EmployeeId",
                table: "StockMovemenet");

            migrationBuilder.DropForeignKey(
                name: "FK_StockMovemenet_Stock_FromStockId",
                table: "StockMovemenet");

            migrationBuilder.DropForeignKey(
                name: "FK_StockMovemenet_Stock_ToStockId",
                table: "StockMovemenet");

            migrationBuilder.DropForeignKey(
                name: "FK_StockMovemenet_StockDocuments_StockDocumentId",
                table: "StockMovemenet");

            migrationBuilder.DropForeignKey(
                name: "FK_StockMovemenet_StockMovementReasons_StockMovementReasonId",
                table: "StockMovemenet");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StockMovemenet",
                table: "StockMovemenet");

            migrationBuilder.RenameTable(
                name: "StockMovemenet",
                newName: "StockMovement");

            migrationBuilder.RenameIndex(
                name: "IX_StockMovemenet_ToStockId",
                table: "StockMovement",
                newName: "IX_StockMovement_ToStockId");

            migrationBuilder.RenameIndex(
                name: "IX_StockMovemenet_StockMovementReasonId",
                table: "StockMovement",
                newName: "IX_StockMovement_StockMovementReasonId");

            migrationBuilder.RenameIndex(
                name: "IX_StockMovemenet_StockDocumentId",
                table: "StockMovement",
                newName: "IX_StockMovement_StockDocumentId");

            migrationBuilder.RenameIndex(
                name: "IX_StockMovemenet_FromStockId",
                table: "StockMovement",
                newName: "IX_StockMovement_FromStockId");

            migrationBuilder.RenameIndex(
                name: "IX_StockMovemenet_EmployeeId",
                table: "StockMovement",
                newName: "IX_StockMovement_EmployeeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StockMovement",
                table: "StockMovement",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StockMovement_Employees_EmployeeId",
                table: "StockMovement",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StockMovement_Stock_FromStockId",
                table: "StockMovement",
                column: "FromStockId",
                principalTable: "Stock",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StockMovement_Stock_ToStockId",
                table: "StockMovement",
                column: "ToStockId",
                principalTable: "Stock",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StockMovement_StockDocuments_StockDocumentId",
                table: "StockMovement",
                column: "StockDocumentId",
                principalTable: "StockDocuments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StockMovement_StockMovementReasons_StockMovementReasonId",
                table: "StockMovement",
                column: "StockMovementReasonId",
                principalTable: "StockMovementReasons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockMovement_Employees_EmployeeId",
                table: "StockMovement");

            migrationBuilder.DropForeignKey(
                name: "FK_StockMovement_Stock_FromStockId",
                table: "StockMovement");

            migrationBuilder.DropForeignKey(
                name: "FK_StockMovement_Stock_ToStockId",
                table: "StockMovement");

            migrationBuilder.DropForeignKey(
                name: "FK_StockMovement_StockDocuments_StockDocumentId",
                table: "StockMovement");

            migrationBuilder.DropForeignKey(
                name: "FK_StockMovement_StockMovementReasons_StockMovementReasonId",
                table: "StockMovement");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StockMovement",
                table: "StockMovement");

            migrationBuilder.RenameTable(
                name: "StockMovement",
                newName: "StockMovemenet");

            migrationBuilder.RenameIndex(
                name: "IX_StockMovement_ToStockId",
                table: "StockMovemenet",
                newName: "IX_StockMovemenet_ToStockId");

            migrationBuilder.RenameIndex(
                name: "IX_StockMovement_StockMovementReasonId",
                table: "StockMovemenet",
                newName: "IX_StockMovemenet_StockMovementReasonId");

            migrationBuilder.RenameIndex(
                name: "IX_StockMovement_StockDocumentId",
                table: "StockMovemenet",
                newName: "IX_StockMovemenet_StockDocumentId");

            migrationBuilder.RenameIndex(
                name: "IX_StockMovement_FromStockId",
                table: "StockMovemenet",
                newName: "IX_StockMovemenet_FromStockId");

            migrationBuilder.RenameIndex(
                name: "IX_StockMovement_EmployeeId",
                table: "StockMovemenet",
                newName: "IX_StockMovemenet_EmployeeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StockMovemenet",
                table: "StockMovemenet",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StockMovemenet_Employees_EmployeeId",
                table: "StockMovemenet",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

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

            migrationBuilder.AddForeignKey(
                name: "FK_StockMovemenet_StockDocuments_StockDocumentId",
                table: "StockMovemenet",
                column: "StockDocumentId",
                principalTable: "StockDocuments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StockMovemenet_StockMovementReasons_StockMovementReasonId",
                table: "StockMovemenet",
                column: "StockMovementReasonId",
                principalTable: "StockMovementReasons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
