using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ElBayt.Infra.Migrations
{
    public partial class AddClothesToDb3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropTable(
                name: "ProductCategories");

            migrationBuilder.DropTable(
                name: "ProductDepartments");

            migrationBuilder.DropTable(
                name: "ProductTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Products",
                table: "Products");

            migrationBuilder.RenameTable(
                name: "Products",
                newName: "ProductModel");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "dbo",
                table: "ClothType",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                schema: "dbo",
                table: "ClothType",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                schema: "dbo",
                table: "ClothType",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                schema: "dbo",
                table: "ClothType",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "dbo",
                table: "ClothType",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "dbo",
                table: "ClothDepartment",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                schema: "dbo",
                table: "ClothDepartment",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                schema: "dbo",
                table: "ClothDepartment",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                schema: "dbo",
                table: "ClothDepartment",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "dbo",
                table: "ClothDepartment",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "dbo",
                table: "ClothCategory",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                schema: "dbo",
                table: "ClothCategory",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                schema: "dbo",
                table: "ClothCategory",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                schema: "dbo",
                table: "ClothCategory",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "dbo",
                table: "ClothCategory",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductModel",
                table: "ProductModel",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cloth_ProductModel_Id",
                schema: "dbo",
                table: "Cloth",
                column: "Id",
                principalTable: "ProductModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductColor_ProductModel_ProductId",
                schema: "dbo",
                table: "ProductColor",
                column: "ProductId",
                principalTable: "ProductModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImage_ProductModel_ProductId",
                schema: "dbo",
                table: "ProductImage",
                column: "ProductId",
                principalTable: "ProductModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSize_ProductModel_ProductId",
                schema: "dbo",
                table: "ProductSize",
                column: "ProductId",
                principalTable: "ProductModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cloth_ProductModel_Id",
                schema: "dbo",
                table: "Cloth");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductColor_ProductModel_ProductId",
                schema: "dbo",
                table: "ProductColor");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductImage_ProductModel_ProductId",
                schema: "dbo",
                table: "ProductImage");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductSize_ProductModel_ProductId",
                schema: "dbo",
                table: "ProductSize");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductModel",
                table: "ProductModel");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "dbo",
                table: "ClothType");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                schema: "dbo",
                table: "ClothType");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                schema: "dbo",
                table: "ClothType");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                schema: "dbo",
                table: "ClothType");

            migrationBuilder.DropColumn(
                name: "Name",
                schema: "dbo",
                table: "ClothType");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "dbo",
                table: "ClothDepartment");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                schema: "dbo",
                table: "ClothDepartment");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                schema: "dbo",
                table: "ClothDepartment");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                schema: "dbo",
                table: "ClothDepartment");

            migrationBuilder.DropColumn(
                name: "Name",
                schema: "dbo",
                table: "ClothDepartment");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "dbo",
                table: "ClothCategory");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                schema: "dbo",
                table: "ClothCategory");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                schema: "dbo",
                table: "ClothCategory");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                schema: "dbo",
                table: "ClothCategory");

            migrationBuilder.DropColumn(
                name: "Name",
                schema: "dbo",
                table: "ClothCategory");

            migrationBuilder.RenameTable(
                name: "ProductModel",
                newName: "Products");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Products",
                table: "Products",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ProductCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductDepartments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductDepartments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTypes", x => x.Id);
                });

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
    }
}
