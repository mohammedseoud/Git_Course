using Microsoft.EntityFrameworkCore.Migrations;

namespace ElBayt.Infra.Migrations
{
    public partial class AddClothesToDb2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cloth_Product_Id",
                schema: "dbo",
                table: "Cloth");

            migrationBuilder.DropForeignKey(
                name: "FK_ClothCategory_ProductCategory_Id",
                schema: "dbo",
                table: "ClothCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_ClothDepartment_ProductDepartment_Id",
                schema: "dbo",
                table: "ClothDepartment");

            migrationBuilder.DropForeignKey(
                name: "FK_ClothType_ProductType_Id",
                schema: "dbo",
                table: "ClothType");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductColor_Product_ProductId",
                schema: "dbo",
                table: "ProductColor");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductImage_Product_ProductId",
                schema: "dbo",
                table: "ProductImage");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductSize_Product_ProductId",
                schema: "dbo",
                table: "ProductSize");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductType",
                schema: "dbo",
                table: "ProductType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductDepartment",
                schema: "dbo",
                table: "ProductDepartment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductCategory",
                schema: "dbo",
                table: "ProductCategory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Product",
                schema: "dbo",
                table: "Product");

            migrationBuilder.RenameTable(
                name: "ProductType",
                schema: "dbo",
                newName: "ProductTypes");

            migrationBuilder.RenameTable(
                name: "ProductDepartment",
                schema: "dbo",
                newName: "ProductDepartments");

            migrationBuilder.RenameTable(
                name: "ProductCategory",
                schema: "dbo",
                newName: "ProductCategories");

            migrationBuilder.RenameTable(
                name: "Product",
                schema: "dbo",
                newName: "Products");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductTypes",
                table: "ProductTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductDepartments",
                table: "ProductDepartments",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductCategories",
                table: "ProductCategories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Products",
                table: "Products",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cloth_Products_Id",
                schema: "dbo",
                table: "Cloth",
                column: "Id",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ClothCategory_ProductCategories_Id",
                schema: "dbo",
                table: "ClothCategory",
                column: "Id",
                principalTable: "ProductCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ClothDepartment_ProductDepartments_Id",
                schema: "dbo",
                table: "ClothDepartment",
                column: "Id",
                principalTable: "ProductDepartments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ClothType_ProductTypes_Id",
                schema: "dbo",
                table: "ClothType",
                column: "Id",
                principalTable: "ProductTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductColor_Products_ProductId",
                schema: "dbo",
                table: "ProductColor",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImage_Products_ProductId",
                schema: "dbo",
                table: "ProductImage",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSize_Products_ProductId",
                schema: "dbo",
                table: "ProductSize",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cloth_Products_Id",
                schema: "dbo",
                table: "Cloth");

            migrationBuilder.DropForeignKey(
                name: "FK_ClothCategory_ProductCategories_Id",
                schema: "dbo",
                table: "ClothCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_ClothDepartment_ProductDepartments_Id",
                schema: "dbo",
                table: "ClothDepartment");

            migrationBuilder.DropForeignKey(
                name: "FK_ClothType_ProductTypes_Id",
                schema: "dbo",
                table: "ClothType");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductColor_Products_ProductId",
                schema: "dbo",
                table: "ProductColor");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductImage_Products_ProductId",
                schema: "dbo",
                table: "ProductImage");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductSize_Products_ProductId",
                schema: "dbo",
                table: "ProductSize");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductTypes",
                table: "ProductTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Products",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductDepartments",
                table: "ProductDepartments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductCategories",
                table: "ProductCategories");

            migrationBuilder.RenameTable(
                name: "ProductTypes",
                newName: "ProductType",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "Products",
                newName: "Product",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "ProductDepartments",
                newName: "ProductDepartment",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "ProductCategories",
                newName: "ProductCategory",
                newSchema: "dbo");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductType",
                schema: "dbo",
                table: "ProductType",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Product",
                schema: "dbo",
                table: "Product",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductDepartment",
                schema: "dbo",
                table: "ProductDepartment",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductCategory",
                schema: "dbo",
                table: "ProductCategory",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cloth_Product_Id",
                schema: "dbo",
                table: "Cloth",
                column: "Id",
                principalSchema: "dbo",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ClothCategory_ProductCategory_Id",
                schema: "dbo",
                table: "ClothCategory",
                column: "Id",
                principalSchema: "dbo",
                principalTable: "ProductCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ClothDepartment_ProductDepartment_Id",
                schema: "dbo",
                table: "ClothDepartment",
                column: "Id",
                principalSchema: "dbo",
                principalTable: "ProductDepartment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ClothType_ProductType_Id",
                schema: "dbo",
                table: "ClothType",
                column: "Id",
                principalSchema: "dbo",
                principalTable: "ProductType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductColor_Product_ProductId",
                schema: "dbo",
                table: "ProductColor",
                column: "ProductId",
                principalSchema: "dbo",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImage_Product_ProductId",
                schema: "dbo",
                table: "ProductImage",
                column: "ProductId",
                principalSchema: "dbo",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSize_Product_ProductId",
                schema: "dbo",
                table: "ProductSize",
                column: "ProductId",
                principalSchema: "dbo",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
