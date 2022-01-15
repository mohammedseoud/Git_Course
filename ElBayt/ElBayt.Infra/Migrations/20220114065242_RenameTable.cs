using Microsoft.EntityFrameworkCore.Migrations;

namespace ElBayt.Infra.Migrations
{
    public partial class RenameTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProducImage_Product_ProductId",
                schema: "dbo",
                table: "ProducImage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProducImage",
                schema: "dbo",
                table: "ProducImage");

            migrationBuilder.RenameTable(
                name: "ProducImage",
                schema: "dbo",
                newName: "ProductImage",
                newSchema: "dbo");

            migrationBuilder.RenameIndex(
                name: "IX_ProducImage_ProductId",
                schema: "dbo",
                table: "ProductImage",
                newName: "IX_ProductImage_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductImage",
                schema: "dbo",
                table: "ProductImage",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImage_Product_ProductId",
                schema: "dbo",
                table: "ProductImage",
                column: "ProductId",
                principalSchema: "dbo",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductImage_Product_ProductId",
                schema: "dbo",
                table: "ProductImage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductImage",
                schema: "dbo",
                table: "ProductImage");

            migrationBuilder.RenameTable(
                name: "ProductImage",
                schema: "dbo",
                newName: "ProducImage",
                newSchema: "dbo");

            migrationBuilder.RenameIndex(
                name: "IX_ProductImage_ProductId",
                schema: "dbo",
                table: "ProducImage",
                newName: "IX_ProducImage_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProducImage",
                schema: "dbo",
                table: "ProducImage",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProducImage_Product_ProductId",
                schema: "dbo",
                table: "ProducImage",
                column: "ProductId",
                principalSchema: "dbo",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
