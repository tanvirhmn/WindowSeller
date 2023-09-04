using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockModule.DAL.Migrations
{
    public partial class renameReasonColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockMovement_StockDocuments_StockDocumentId",
                table: "StockMovement");

            migrationBuilder.DropForeignKey(
                name: "FK_StockMovement_StockMovementReasons_StockMovementReasonId",
                table: "StockMovement");

            migrationBuilder.DropIndex(
                name: "IX_StockMovement_StockDocumentId",
                table: "StockMovement");

            migrationBuilder.DropColumn(
                name: "StockDocumentId",
                table: "StockMovement");

            migrationBuilder.RenameColumn(
                name: "StockMovementReasonId",
                table: "StockMovement",
                newName: "DocumentId");

            migrationBuilder.RenameIndex(
                name: "IX_StockMovement_StockMovementReasonId",
                table: "StockMovement",
                newName: "IX_StockMovement_DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_StockMovement_ReasonId",
                table: "StockMovement",
                column: "ReasonId");

            migrationBuilder.AddForeignKey(
                name: "FK_StockMovement_StockDocuments_DocumentId",
                table: "StockMovement",
                column: "DocumentId",
                principalTable: "StockDocuments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StockMovement_StockMovementReasons_ReasonId",
                table: "StockMovement",
                column: "ReasonId",
                principalTable: "StockMovementReasons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockMovement_StockDocuments_DocumentId",
                table: "StockMovement");

            migrationBuilder.DropForeignKey(
                name: "FK_StockMovement_StockMovementReasons_ReasonId",
                table: "StockMovement");

            migrationBuilder.DropIndex(
                name: "IX_StockMovement_ReasonId",
                table: "StockMovement");

            migrationBuilder.RenameColumn(
                name: "DocumentId",
                table: "StockMovement",
                newName: "StockMovementReasonId");

            migrationBuilder.RenameIndex(
                name: "IX_StockMovement_DocumentId",
                table: "StockMovement",
                newName: "IX_StockMovement_StockMovementReasonId");

            migrationBuilder.AddColumn<int>(
                name: "StockDocumentId",
                table: "StockMovement",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_StockMovement_StockDocumentId",
                table: "StockMovement",
                column: "StockDocumentId");

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
    }
}
