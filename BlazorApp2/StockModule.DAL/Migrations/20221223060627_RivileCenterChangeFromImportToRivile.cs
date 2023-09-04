using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockModule.DAL.Migrations
{
    /// <inheritdoc />
    public partial class RivileCenterChangeFromImportToRivile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImportToRivile",
                table: "StockMovementReasons");

            migrationBuilder.AddColumn<string>(
                name: "RivileCenter",
                table: "StockMovementReasons",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RivileCenter",
                table: "StockMovementReasons");

            migrationBuilder.AddColumn<bool>(
                name: "ImportToRivile",
                table: "StockMovementReasons",
                type: "bit",
                nullable: true,
                defaultValue: true);
        }
    }
}
