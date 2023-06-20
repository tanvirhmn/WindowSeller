using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockModule.DAL.Migrations
{
    public partial class userPermissionNameDelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "UserPermissions");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "UserPermissions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
