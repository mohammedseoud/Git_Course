using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ElBayt.Infra.Migrations
{
    public partial class AddClothCategorySize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClothSizes",
                schema: "dbo");

            migrationBuilder.CreateTable(
                name: "ClothCategorySizes",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClothCategoryId = table.Column<int>(type: "int", nullable: false),
                    SizeId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClothCategorySizes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClothCategorySizes_ClothCategory_ClothCategoryId",
                        column: x => x.ClothCategoryId,
                        principalSchema: "dbo",
                        principalTable: "ClothCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClothCategorySizes_ClothSize_SizeId",
                        column: x => x.SizeId,
                        principalSchema: "dbo",
                        principalTable: "ClothSize",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClothCategorySizes_ClothCategoryId",
                schema: "dbo",
                table: "ClothCategorySizes",
                column: "ClothCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ClothCategorySizes_SizeId",
                schema: "dbo",
                table: "ClothCategorySizes",
                column: "SizeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClothCategorySizes",
                schema: "dbo");

            migrationBuilder.CreateTable(
                name: "ClothSizes",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClothId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SizeId = table.Column<int>(type: "int", nullable: false)
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
