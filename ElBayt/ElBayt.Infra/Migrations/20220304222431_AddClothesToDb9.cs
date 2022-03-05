using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ElBayt.Infra.Migrations
{
    public partial class AddClothesToDb9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cloth",
                schema: "dbo");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "Cloth",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClothCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cloth", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cloth_ClothCategory_ClothCategoryId",
                        column: x => x.ClothCategoryId,
                        principalSchema: "dbo",
                        principalTable: "ClothCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cloth_ProductModel_Id",
                        column: x => x.Id,
                        principalTable: "ProductModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cloth_ClothCategoryId",
                schema: "dbo",
                table: "Cloth",
                column: "ClothCategoryId");
        }
    }
}
