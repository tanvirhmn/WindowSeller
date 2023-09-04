using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockModule.DAL.Migrations
{
    public partial class StockMovementToTotalQuantity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TotalQuantity",
                table: "StockMovement",
                newName: "ToTotalQuantity");

            migrationBuilder.AddColumn<double>(
                name: "FromTotalQuantity",
                table: "StockMovement",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FromTotalQuantity",
                table: "StockMovement");

            migrationBuilder.RenameColumn(
                name: "ToTotalQuantity",
                table: "StockMovement",
                newName: "TotalQuantity");
        }
    }
}
