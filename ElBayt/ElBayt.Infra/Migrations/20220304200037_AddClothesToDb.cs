using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ElBayt.Infra.Migrations
{
    public partial class AddClothesToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_ProductCategory_ProductCategoryId",
                schema: "dbo",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductCategory_ProductType_ProductTypeId",
                schema: "dbo",
                table: "ProductCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductType_ProductDepartment_DepartmentId",
                schema: "dbo",
                table: "ProductType");

            migrationBuilder.DropIndex(
                name: "IX_ProductType_DepartmentId",
                schema: "dbo",
                table: "ProductType");

            migrationBuilder.DropIndex(
                name: "IX_ProductCategory_ProductTypeId",
                schema: "dbo",
                table: "ProductCategory");

            migrationBuilder.DropIndex(
                name: "IX_Product_ProductCategoryId",
                schema: "dbo",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                schema: "dbo",
                table: "ProductType");

            migrationBuilder.DropColumn(
                name: "ProductTypeId",
                schema: "dbo",
                table: "ProductCategory");

            migrationBuilder.DropColumn(
                name: "ProductCategoryId",
                schema: "dbo",
                table: "Product");

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

            migrationBuilder.CreateTable(
                name: "ClothDepartment",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClothDepartment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClothDepartment_ProductDepartment_Id",
                        column: x => x.Id,
                        principalSchema: "dbo",
                        principalTable: "ProductDepartment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ClothType",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClothTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClothType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClothType_ClothDepartment_ClothTypeId",
                        column: x => x.ClothTypeId,
                        principalSchema: "dbo",
                        principalTable: "ClothDepartment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClothType_ProductType_Id",
                        column: x => x.Id,
                        principalSchema: "dbo",
                        principalTable: "ProductType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ClothCategory",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClothTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClothCategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClothCategory_ClothType_ClothTypeId",
                        column: x => x.ClothTypeId,
                        principalSchema: "dbo",
                        principalTable: "ClothType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClothCategory_ProductCategory_Id",
                        column: x => x.Id,
                        principalSchema: "dbo",
                        principalTable: "ProductCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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
                        name: "FK_Cloth_Product_Id",
                        column: x => x.Id,
                        principalSchema: "dbo",
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cloth_ClothCategoryId",
                schema: "dbo",
                table: "Cloth",
                column: "ClothCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ClothCategory_ClothTypeId",
                schema: "dbo",
                table: "ClothCategory",
                column: "ClothTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ClothType_ClothTypeId",
                schema: "dbo",
                table: "ClothType",
                column: "ClothTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cloth",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "ClothCategory",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "ClothType",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "ClothDepartment",
                schema: "dbo");

            migrationBuilder.AddColumn<Guid>(
                name: "DepartmentId",
                schema: "dbo",
                table: "ProductType",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

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
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "Reviews",
                schema: "dbo",
                table: "Product",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ProductType_DepartmentId",
                schema: "dbo",
                table: "ProductType",
                column: "DepartmentId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Product_ProductCategory_ProductCategoryId",
                schema: "dbo",
                table: "Product",
                column: "ProductCategoryId",
                principalSchema: "dbo",
                principalTable: "ProductCategory",
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

            migrationBuilder.AddForeignKey(
                name: "FK_ProductType_ProductDepartment_DepartmentId",
                schema: "dbo",
                table: "ProductType",
                column: "DepartmentId",
                principalSchema: "dbo",
                principalTable: "ProductDepartment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
