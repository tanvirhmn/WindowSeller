using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockModule.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddView_StockSettinsMaterialView_FolderHierarchy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE VIEW [dbo].[View_StockSettingsMaterial_FolderHierarchy]
                    AS
                    SELECT dbo.Materials.Id, dbo.Materials.Code, dbo.Materials.Alias, dbo.Materials.Description, dbo.Materials.Color, dbo.Materials.Type, dbo.Materials.BarLength, dbo.StockSettings.Id AS StockSettingsId,
                         dbo.StockSettings.FolderHierarchyId
                    FROM dbo.Materials LEFT OUTER JOIN
                         dbo.StockSettings ON dbo.Materials.Id = dbo.StockSettings.MaterialId");
        }
    

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP VIEW [dbo].[View_StockSettingsMaterial_FolderHierarchy];");
        }
    }
}
