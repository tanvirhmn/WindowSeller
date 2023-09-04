using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockModule.DAL.Migrations
{
    /// <inheritdoc />
    public partial class stockSettingMaterialForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_StockSettings_MaterialId",
                table: "StockSettings");

            migrationBuilder.CreateIndex(
                name: "IX_StockSettings_MaterialId",
                table: "StockSettings",
                column: "MaterialId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_StockSettings_MaterialId",
                table: "StockSettings");

            migrationBuilder.CreateIndex(
                name: "IX_StockSettings_MaterialId",
                table: "StockSettings",
                column: "MaterialId");
        }
    }
}
