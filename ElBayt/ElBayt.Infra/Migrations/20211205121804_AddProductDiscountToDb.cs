using Microsoft.EntityFrameworkCore.Migrations;

namespace ElBayt.Infra.Migrations
{
    public partial class AddProductDiscountToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PriceAfterDiscout",
                schema: "dbo",
                table: "Product",
                newName: "PriceAfterDiscount");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PriceAfterDiscount",
                schema: "dbo",
                table: "Product",
                newName: "PriceAfterDiscout");
        }
    }
}
