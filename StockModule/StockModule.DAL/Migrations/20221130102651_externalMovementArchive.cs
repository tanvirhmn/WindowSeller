using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockModule.DAL.Migrations
{
    public partial class externalMovementArchive : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "ExternalMovements");

            migrationBuilder.CreateTable(
                name: "ExternalMovementsArchive",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaterialCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Length = table.Column<double>(type: "float", nullable: false),
                    Height = table.Column<double>(type: "float", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<double>(type: "float", nullable: false),
                    FromWarehouse = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ToWarehouse = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DocumentNumber = table.Column<int>(type: "int", nullable: false),
                    DocumentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExternalMovementsArchive", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExternalMovementsArchive");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "ExternalMovements",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
