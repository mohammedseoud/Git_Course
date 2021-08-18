using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ElBayt.Infra.Migrations
{
    public partial class AddSomeRelationsToDB_4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ProductCategoryId",
                schema: "dbo",
                table: "Product",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Product_ProductCategoryId",
                schema: "dbo",
                table: "Product",
                column: "ProductCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_ProductCategory_ProductCategoryId",
                schema: "dbo",
                table: "Product",
                column: "ProductCategoryId",
                principalSchema: "dbo",
                principalTable: "ProductCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_ProductCategory_ProductCategoryId",
                schema: "dbo",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_ProductCategoryId",
                schema: "dbo",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "ProductCategoryId",
                schema: "dbo",
                table: "Product");
        }
    }
}
