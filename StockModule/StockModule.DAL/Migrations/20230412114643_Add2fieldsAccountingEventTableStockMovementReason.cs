using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockModule.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Add2fieldsAccountingEventTableStockMovementReason : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AccountingEventCanceledReasonId",
                table: "StockMovementReasons",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsGenerateAccountingEvent",
                table: "StockMovementReasons",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountingEventCanceledReasonId",
                table: "StockMovementReasons");

            migrationBuilder.DropColumn(
                name: "IsGenerateAccountingEvent",
                table: "StockMovementReasons");
        }
    }
}
