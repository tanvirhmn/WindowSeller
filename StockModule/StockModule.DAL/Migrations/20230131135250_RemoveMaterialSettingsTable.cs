using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockModule.DAL.Migrations
{
    /// <inheritdoc />
    public partial class RemoveMaterialSettingsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MaterialsSettings");

            migrationBuilder.AlterColumn<bool>(
                name: "Reproducible",
                table: "StockSettings",
                type: "bit",
                nullable: false,
                defaultValueSql: "(1)",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CollectionType",
                table: "StockSettings",
                type: "varchar(250)",
                unicode: false,
                maxLength: 250,
                nullable: false,
                defaultValueSql: "('Warehouse')",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "Reproducible",
                table: "StockSettings",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValueSql: "(1)");

            migrationBuilder.AlterColumn<string>(
                name: "CollectionType",
                table: "StockSettings",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(250)",
                oldUnicode: false,
                oldMaxLength: 250,
                oldDefaultValueSql: "('Warehouse')");

            migrationBuilder.CreateTable(
                name: "MaterialsSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaterialId = table.Column<int>(type: "int", nullable: false),
                    CollectionType = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: false, defaultValueSql: "('Warehouse')"),
                    Reproducible = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "(1)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialsSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaterialsSettings_Materials_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Materials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MaterialsSettings_MaterialId",
                table: "MaterialsSettings",
                column: "MaterialId",
                unique: true);
        }
    }
}
