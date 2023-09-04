using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockModule.DAL.Migrations
{
    public partial class externalMovementRemoveWarehouses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FromWarehouse",
                table: "ExternalMovementsArchive");

            migrationBuilder.DropColumn(
                name: "Reason",
                table: "ExternalMovementsArchive");

            migrationBuilder.DropColumn(
                name: "FromWarehouse",
                table: "ExternalMovements");

            migrationBuilder.DropColumn(
                name: "Reason",
                table: "ExternalMovements");

            migrationBuilder.RenameColumn(
                name: "ToWarehouse",
                table: "ExternalMovementsArchive",
                newName: "ReasonName");

            migrationBuilder.RenameColumn(
                name: "ToWarehouse",
                table: "ExternalMovements",
                newName: "ReasonName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReasonName",
                table: "ExternalMovementsArchive",
                newName: "ToWarehouse");

            migrationBuilder.RenameColumn(
                name: "ReasonName",
                table: "ExternalMovements",
                newName: "ToWarehouse");

            migrationBuilder.AddColumn<string>(
                name: "FromWarehouse",
                table: "ExternalMovementsArchive",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Reason",
                table: "ExternalMovementsArchive",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FromWarehouse",
                table: "ExternalMovements",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Reason",
                table: "ExternalMovements",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
