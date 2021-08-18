using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ElBayt.Infra.Migrations
{
    public partial class AddSomeRelationsToDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ProductTypeId",
                schema: "dbo",
                table: "ProductCategory",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ProductCategoryId",
                schema: "dbo",
                table: "Product",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CountryId",
                schema: "dbo",
                table: "Governorate",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "GovernorateId",
                schema: "dbo",
                table: "Area",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategory_ProductTypeId",
                schema: "dbo",
                table: "ProductCategory",
                column: "ProductTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_ProductCategoryId",
                schema: "dbo",
                table: "Product",
                column: "ProductCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Governorate_CountryId",
                schema: "dbo",
                table: "Governorate",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Area_GovernorateId",
                schema: "dbo",
                table: "Area",
                column: "GovernorateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Area_Governorate_GovernorateId",
                schema: "dbo",
                table: "Area",
                column: "GovernorateId",
                principalSchema: "dbo",
                principalTable: "Governorate",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Governorate_Country_CountryId",
                schema: "dbo",
                table: "Governorate",
                column: "CountryId",
                principalSchema: "dbo",
                principalTable: "Country",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);


            migrationBuilder.AddForeignKey(
                name: "FK_ProductCategory_ProductType_ProductTypeId",
                schema: "dbo",
                table: "ProductCategory",
                column: "ProductTypeId",
                principalSchema: "dbo",
                principalTable: "ProductType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Area_Governorate_GovernorateId",
                schema: "dbo",
                table: "Area");

            migrationBuilder.DropForeignKey(
                name: "FK_Governorate_Country_CountryId",
                schema: "dbo",
                table: "Governorate");

          
            migrationBuilder.DropForeignKey(
                name: "FK_ProductCategory_ProductType_ProductTypeId",
                schema: "dbo",
                table: "ProductCategory");

            migrationBuilder.DropIndex(
                name: "IX_ProductCategory_ProductTypeId",
                schema: "dbo",
                table: "ProductCategory");

            migrationBuilder.DropIndex(
                name: "IX_Product_ProductCategoryId",
                schema: "dbo",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Governorate_CountryId",
                schema: "dbo",
                table: "Governorate");

            migrationBuilder.DropIndex(
                name: "IX_Area_GovernorateId",
                schema: "dbo",
                table: "Area");

            migrationBuilder.DropColumn(
                name: "ProductTypeId",
                schema: "dbo",
                table: "ProductCategory");

            migrationBuilder.DropColumn(
                name: "ProductCategoryId",
                schema: "dbo",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "CountryId",
                schema: "dbo",
                table: "Governorate");

            migrationBuilder.DropColumn(
                name: "GovernorateId",
                schema: "dbo",
                table: "Area");
        }
    }
}
