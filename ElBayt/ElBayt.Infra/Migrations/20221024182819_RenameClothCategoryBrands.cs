using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ElBayt.Infra.Migrations
{
    public partial class RenameClothCategoryBrands : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClothBrands",
                schema: "dbo");

            migrationBuilder.CreateTable(
                name: "ClothCategoryBrands",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClothCategoryId = table.Column<int>(type: "int", nullable: false),
                    BrandId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClothCategoryBrands", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClothCategoryBrands_ClothBrand_BrandId",
                        column: x => x.BrandId,
                        principalSchema: "dbo",
                        principalTable: "ClothBrand",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClothCategoryBrands_ClothCategory_ClothCategoryId",
                        column: x => x.ClothCategoryId,
                        principalSchema: "dbo",
                        principalTable: "ClothCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClothCategoryBrands_BrandId",
                schema: "dbo",
                table: "ClothCategoryBrands",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_ClothCategoryBrands_ClothCategoryId",
                schema: "dbo",
                table: "ClothCategoryBrands",
                column: "ClothCategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClothCategoryBrands",
                schema: "dbo");

            migrationBuilder.CreateTable(
                name: "ClothBrands",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrandId = table.Column<int>(type: "int", nullable: false),
                    ClothCategoryId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClothBrands", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClothBrands_ClothBrand_BrandId",
                        column: x => x.BrandId,
                        principalSchema: "dbo",
                        principalTable: "ClothBrand",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClothBrands_ClothCategory_ClothCategoryId",
                        column: x => x.ClothCategoryId,
                        principalSchema: "dbo",
                        principalTable: "ClothCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClothBrands_BrandId",
                schema: "dbo",
                table: "ClothBrands",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_ClothBrands_ClothCategoryId",
                schema: "dbo",
                table: "ClothBrands",
                column: "ClothCategoryId");
        }
    }
}
