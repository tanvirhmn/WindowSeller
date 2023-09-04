using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockModule.DAL.Migrations
{
    /// <inheritdoc />
    public partial class MaterialSettingsToStockSettings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MaterialsSetting_Materials_MaterialId",
                table: "MaterialsSetting");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MaterialsSetting",
                table: "MaterialsSetting");

            migrationBuilder.RenameTable(
                name: "MaterialsSetting",
                newName: "MaterialsSettings");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MaterialsSettings",
                table: "MaterialsSettings",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "StockSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaterialId = table.Column<int>(type: "int", nullable: false),
                    CollectionType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Reproducible = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StockSettings_Materials_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Materials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StockSettings_MaterialId",
                table: "StockSettings",
                column: "MaterialId");

            migrationBuilder.AddForeignKey(
                name: "FK_MaterialsSettings_Materials_MaterialId",
                table: "MaterialsSettings",
                column: "MaterialId",
                principalTable: "Materials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MaterialsSettings_Materials_MaterialId",
                table: "MaterialsSettings");

            migrationBuilder.DropTable(
                name: "StockSettings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MaterialsSettings",
                table: "MaterialsSettings");

            migrationBuilder.RenameTable(
                name: "MaterialsSettings",
                newName: "MaterialsSetting");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MaterialsSetting",
                table: "MaterialsSetting",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MaterialsSetting_Materials_MaterialId",
                table: "MaterialsSetting",
                column: "MaterialId",
                principalTable: "Materials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
