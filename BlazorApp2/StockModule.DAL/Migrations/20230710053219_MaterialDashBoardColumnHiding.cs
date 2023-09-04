using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockModule.DAL.Migrations
{
    /// <inheritdoc />
    public partial class MaterialDashBoardColumnHiding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DynamicMaterialCoulmnGridHiding",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaterialColumnId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DynamicMaterialCoulmnGridHiding", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DynamicMaterialCoulmnGridHiding_MaterialColumn_MaterialColumnId",
                        column: x => x.MaterialColumnId,
                        principalTable: "MaterialColumn",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DynamicMaterialCoulmnGridHiding_MaterialColumnId",
                table: "DynamicMaterialCoulmnGridHiding",
                column: "MaterialColumnId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DynamicMaterialCoulmnGridHiding");
        }
    }
}
