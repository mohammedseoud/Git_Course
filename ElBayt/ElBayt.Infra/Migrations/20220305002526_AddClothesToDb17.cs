using Microsoft.EntityFrameworkCore.Migrations;

namespace ElBayt.Infra.Migrations
{
    public partial class AddClothesToDb17 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductImageURL3",
                schema: "dbo",
                table: "Cloth");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProductImageURL3",
                schema: "dbo",
                table: "Cloth",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);
        }
    }
}
