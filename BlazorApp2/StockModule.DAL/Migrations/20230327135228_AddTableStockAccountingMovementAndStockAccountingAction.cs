using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockModule.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddTableStockAccountingMovementAndStockAccountingAction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StockAccountingMovements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StockMovementId = table.Column<int>(type: "int", nullable: false),
                    ChangedByStockMovementId = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockAccountingMovements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StockAccountingMovements_StockMovement_ChangedByStockMovementId",
                        column: x => x.ChangedByStockMovementId,
                        principalTable: "StockMovement",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StockAccountingMovements_StockMovement_StockMovementId",
                        column: x => x.StockMovementId,
                        principalTable: "StockMovement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StockAccountingActions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StockAccountingMovementId = table.Column<int>(type: "int", nullable: false),
                    Request = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Response = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockAccountingActions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StockAccountingActions_StockAccountingMovements_StockAccountingMovementId",
                        column: x => x.StockAccountingMovementId,
                        principalTable: "StockAccountingMovements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StockAccountingActions_StockAccountingMovementId",
                table: "StockAccountingActions",
                column: "StockAccountingMovementId");

            migrationBuilder.CreateIndex(
                name: "IX_StockAccountingMovements_ChangedByStockMovementId",
                table: "StockAccountingMovements",
                column: "ChangedByStockMovementId");

            migrationBuilder.CreateIndex(
                name: "IX_StockAccountingMovements_StockMovementId",
                table: "StockAccountingMovements",
                column: "StockMovementId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StockAccountingActions");

            migrationBuilder.DropTable(
                name: "StockAccountingMovements");
        }
    }
}
