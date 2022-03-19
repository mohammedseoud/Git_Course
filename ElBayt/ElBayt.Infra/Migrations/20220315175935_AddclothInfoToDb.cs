using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ElBayt.Infra.Migrations
{
    public partial class AddclothInfoToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClothBrands",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "ClothColors",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "ClothSizes",
                schema: "dbo");

            migrationBuilder.CreateTable(
                name: "ClothInfo",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClothId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SizeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ColorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    BrandId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClothInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClothInfo_Cloth_ClothId",
                        column: x => x.ClothId,
                        principalSchema: "dbo",
                        principalTable: "Cloth",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClothInfo_ClothBrand_BrandId",
                        column: x => x.BrandId,
                        principalSchema: "dbo",
                        principalTable: "ClothBrand",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClothInfo_ClothSize_SizeId",
                        column: x => x.SizeId,
                        principalSchema: "dbo",
                        principalTable: "ClothSize",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClothInfo_Color_ColorId",
                        column: x => x.ColorId,
                        principalSchema: "dbo",
                        principalTable: "Color",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClothInfo_BrandId",
                schema: "dbo",
                table: "ClothInfo",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_ClothInfo_ClothId",
                schema: "dbo",
                table: "ClothInfo",
                column: "ClothId");

            migrationBuilder.CreateIndex(
                name: "IX_ClothInfo_ColorId",
                schema: "dbo",
                table: "ClothInfo",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_ClothInfo_SizeId",
                schema: "dbo",
                table: "ClothInfo",
                column: "SizeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClothInfo",
                schema: "dbo");

            migrationBuilder.CreateTable(
                name: "ClothBrands",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    BrandId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClothId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClothBrands", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClothBrands_Cloth_ClothId",
                        column: x => x.ClothId,
                        principalSchema: "dbo",
                        principalTable: "Cloth",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClothBrands_ClothBrand_BrandId",
                        column: x => x.BrandId,
                        principalSchema: "dbo",
                        principalTable: "ClothBrand",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClothColors",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClothId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ColorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClothColors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClothColors_Cloth_ClothId",
                        column: x => x.ClothId,
                        principalSchema: "dbo",
                        principalTable: "Cloth",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClothColors_Color_ColorId",
                        column: x => x.ColorId,
                        principalSchema: "dbo",
                        principalTable: "Color",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClothSizes",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    ClothId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SizeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClothSizes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClothSizes_Cloth_ClothId",
                        column: x => x.ClothId,
                        principalSchema: "dbo",
                        principalTable: "Cloth",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClothSizes_ClothSize_SizeId",
                        column: x => x.SizeId,
                        principalSchema: "dbo",
                        principalTable: "ClothSize",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClothBrands_BrandId",
                schema: "dbo",
                table: "ClothBrands",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_ClothBrands_ClothId",
                schema: "dbo",
                table: "ClothBrands",
                column: "ClothId");

            migrationBuilder.CreateIndex(
                name: "IX_ClothColors_ClothId",
                schema: "dbo",
                table: "ClothColors",
                column: "ClothId");

            migrationBuilder.CreateIndex(
                name: "IX_ClothColors_ColorId",
                schema: "dbo",
                table: "ClothColors",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_ClothSizes_ClothId",
                schema: "dbo",
                table: "ClothSizes",
                column: "ClothId");

            migrationBuilder.CreateIndex(
                name: "IX_ClothSizes_SizeId",
                schema: "dbo",
                table: "ClothSizes",
                column: "SizeId");
        }
    }
}
