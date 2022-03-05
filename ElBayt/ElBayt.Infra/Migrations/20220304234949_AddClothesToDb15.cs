using Microsoft.EntityFrameworkCore.Migrations;

namespace ElBayt.Infra.Migrations
{
    public partial class AddClothesToDb15 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductImage_ProductModel_ProductId",
                schema: "dbo",
                table: "ProductImage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductModel",
                schema: "dbo",
                table: "ProductModel");

            migrationBuilder.RenameTable(
                name: "ProductModel",
                schema: "dbo",
                newName: "ProductModels",
                newSchema: "dbo");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductModels",
                schema: "dbo",
                table: "ProductModels",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImage_ProductModels_ProductId",
                schema: "dbo",
                table: "ProductImage",
                column: "ProductId",
                principalSchema: "dbo",
                principalTable: "ProductModels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductImage_ProductModels_ProductId",
                schema: "dbo",
                table: "ProductImage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductModels",
                schema: "dbo",
                table: "ProductModels");

            migrationBuilder.RenameTable(
                name: "ProductModels",
                schema: "dbo",
                newName: "ProductModel",
                newSchema: "dbo");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductModel",
                schema: "dbo",
                table: "ProductModel",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImage_ProductModel_ProductId",
                schema: "dbo",
                table: "ProductImage",
                column: "ProductId",
                principalSchema: "dbo",
                principalTable: "ProductModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
