using Microsoft.EntityFrameworkCore.Migrations;

namespace ElBayt.Infra.Migrations
{
    public partial class AddColumnsToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                schema: "dbo",
                table: "Product",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Ratings",
                schema: "dbo",
                table: "Product",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 5);

            migrationBuilder.AddColumn<int>(
                name: "Reviews",
                schema: "dbo",
                table: "Product",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                schema: "dbo",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "Ratings",
                schema: "dbo",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "Reviews",
                schema: "dbo",
                table: "Product");
        }
    }
}
