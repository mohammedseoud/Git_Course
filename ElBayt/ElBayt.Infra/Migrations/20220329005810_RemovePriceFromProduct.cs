using Microsoft.EntityFrameworkCore.Migrations;

namespace ElBayt.Infra.Migrations
{
    public partial class RemovePriceFromProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                schema: "dbo",
                table: "Cloth");

            migrationBuilder.DropColumn(
                name: "PriceAfterDiscount",
                schema: "dbo",
                table: "Cloth");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                schema: "dbo",
                table: "ClothInfo",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PriceAfterDiscount",
                schema: "dbo",
                table: "ClothInfo",
                type: "decimal(18,2)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                schema: "dbo",
                table: "ClothInfo");

            migrationBuilder.DropColumn(
                name: "PriceAfterDiscount",
                schema: "dbo",
                table: "ClothInfo");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                schema: "dbo",
                table: "Cloth",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PriceAfterDiscount",
                schema: "dbo",
                table: "Cloth",
                type: "decimal(18,2)",
                nullable: true);
        }
    }
}
