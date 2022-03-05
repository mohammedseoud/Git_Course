using Microsoft.EntityFrameworkCore.Migrations;

namespace ElBayt.Infra.Migrations
{
    public partial class AddClothesToDb8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "ProductModel",
                type: "nvarchar(max)",
                maxLength: 5000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "ProductModel",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "ProductModel",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PriceAfterDiscount",
                table: "ProductModel",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductImageURL1",
                table: "ProductModel",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProductImageURL2",
                table: "ProductModel",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "ProductModel");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "ProductModel");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "ProductModel");

            migrationBuilder.DropColumn(
                name: "PriceAfterDiscount",
                table: "ProductModel");

            migrationBuilder.DropColumn(
                name: "ProductImageURL1",
                table: "ProductModel");

            migrationBuilder.DropColumn(
                name: "ProductImageURL2",
                table: "ProductModel");
        }
    }
}
