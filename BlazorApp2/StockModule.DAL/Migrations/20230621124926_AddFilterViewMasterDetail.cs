using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockModule.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddFilterViewMasterDetail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FilterViewMaster",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    azureUserID = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilterViewMaster", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FilterViewDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Property = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    FilterValue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FilterOperator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LogicalFilterOperator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FilterViewMasterID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilterViewDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FilterViewDetail_FilterViewDetail_ParentId",
                        column: x => x.ParentId,
                        principalTable: "FilterViewDetail",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FilterViewDetail_FilterViewMaster_FilterViewMasterID",
                        column: x => x.FilterViewMasterID,
                        principalTable: "FilterViewMaster",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });


            migrationBuilder.CreateIndex(
                name: "IX_FilterViewDetail_FilterViewMasterID",
                table: "FilterViewDetail",
                column: "FilterViewMasterID");

            migrationBuilder.CreateIndex(
                name: "IX_FilterViewDetail_ParentId",
                table: "FilterViewDetail",
                column: "ParentId");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropTable(
                name: "FilterViewDetail");

            migrationBuilder.DropTable(
                name: "FilterViewMaster");


        }
    }
}
