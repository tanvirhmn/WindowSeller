using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockModule.DAL.Migrations
{
    /// <inheritdoc />
    public partial class DMCDynamicMaterialCoulmn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MaterialTypeEnumMaster",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialTypeEnumMaster", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MaterialColumn",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Block = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    IsLocked = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    MaterialTypeEnumMasterId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialColumn", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaterialColumn_MaterialTypeEnumMaster_MaterialTypeEnumMasterId",
                        column: x => x.MaterialTypeEnumMasterId,
                        principalTable: "MaterialTypeEnumMaster",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MaterialTypeEnumDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Key = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    MaterialTypeEnumMasterId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialTypeEnumDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaterialTypeEnumDetail_MaterialTypeEnumMaster_MaterialTypeEnumMasterId",
                        column: x => x.MaterialTypeEnumMasterId,
                        principalTable: "MaterialTypeEnumMaster",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FolderHierarchyMaterialColumnMap",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsRequired = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    IsVisible = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    FolderHierarchyId = table.Column<int>(type: "int", nullable: false),
                    MaterialColumnId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FolderHierarchyMaterialColumnMap", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FolderHierarchyMaterialColumnMap_FolderHierarchy_FolderHierarchyId",
                        column: x => x.FolderHierarchyId,
                        principalTable: "FolderHierarchy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FolderHierarchyMaterialColumnMap_MaterialColumn_MaterialColumnId",
                        column: x => x.MaterialColumnId,
                        principalTable: "MaterialColumn",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MaterialColumnValue",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RowNo = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    MaterialColumnId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialColumnValue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaterialColumnValue_MaterialColumn_MaterialColumnId",
                        column: x => x.MaterialColumnId,
                        principalTable: "MaterialColumn",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FolderHierarchyMaterialColumnMap_FolderHierarchyId",
                table: "FolderHierarchyMaterialColumnMap",
                column: "FolderHierarchyId");

            migrationBuilder.CreateIndex(
                name: "IX_FolderHierarchyMaterialColumnMap_MaterialColumnId",
                table: "FolderHierarchyMaterialColumnMap",
                column: "MaterialColumnId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialColumn_MaterialTypeEnumMasterId",
                table: "MaterialColumn",
                column: "MaterialTypeEnumMasterId",
                unique: true,
                filter: "[MaterialTypeEnumMasterId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialColumnValue_MaterialColumnId",
                table: "MaterialColumnValue",
                column: "MaterialColumnId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialTypeEnumDetail_MaterialTypeEnumMasterId",
                table: "MaterialTypeEnumDetail",
                column: "MaterialTypeEnumMasterId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FolderHierarchyMaterialColumnMap");

            migrationBuilder.DropTable(
                name: "MaterialColumnValue");

            migrationBuilder.DropTable(
                name: "MaterialTypeEnumDetail");

            migrationBuilder.DropTable(
                name: "MaterialColumn");

            migrationBuilder.DropTable(
                name: "MaterialTypeEnumMaster");
        }
    }
}
