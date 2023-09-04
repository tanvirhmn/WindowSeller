using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockModule.DAL.Migrations
{
    public partial class employeeIdToUserName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "ExternalMovementsArchive");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "ExternalMovements");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "ExternalMovementsArchive",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "ExternalMovements",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserName",
                table: "ExternalMovementsArchive");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "ExternalMovements");

            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "ExternalMovementsArchive",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "ExternalMovements",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
