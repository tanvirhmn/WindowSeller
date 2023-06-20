using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockModule.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddFolderHierarchyToStockSettings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FolderHierarchyId",
                table: "StockSettings",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StockSettings_FolderHierarchyId",
                table: "StockSettings",
                column: "FolderHierarchyId");

            migrationBuilder.AddForeignKey(
                name: "FK_StockSettings_FolderHierarchy_FolderHierarchyId",
                table: "StockSettings",
                column: "FolderHierarchyId",
                principalTable: "FolderHierarchy",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockSettings_FolderHierarchy_FolderHierarchyId",
                table: "StockSettings");

            migrationBuilder.DropIndex(
                name: "IX_StockSettings_FolderHierarchyId",
                table: "StockSettings");

            migrationBuilder.DropColumn(
                name: "FolderHierarchyId",
                table: "StockSettings");
        }
    }
}
