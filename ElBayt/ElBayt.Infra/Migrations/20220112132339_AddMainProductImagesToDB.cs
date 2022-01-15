using Microsoft.EntityFrameworkCore.Migrations;

namespace ElBayt.Infra.Migrations
{
    public partial class AddMainProductImagesToDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProductImageURL1",
                schema: "dbo",
                table: "Product",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProductImageURL2",
                schema: "dbo",
                table: "Product",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductImageURL1",
                schema: "dbo",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "ProductImageURL2",
                schema: "dbo",
                table: "Product");
        }
    }
}
