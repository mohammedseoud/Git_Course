using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ElBayt.Infra.Migrations
{
    public partial class AddCategoriesSize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClothSize_ClothCategory_ClothCategoryId",
                schema: "dbo",
                table: "ClothSize");

            migrationBuilder.DropIndex(
                name: "IX_ClothSize_ClothCategoryId",
                schema: "dbo",
                table: "ClothSize");

            migrationBuilder.DropColumn(
                name: "ClothCategoryId",
                schema: "dbo",
                table: "ClothSize");

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

            migrationBuilder.AddColumn<int>(
                name: "ClothCategoryId",
                schema: "dbo",
                table: "ClothSize",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ClothSize_ClothCategoryId",
                schema: "dbo",
                table: "ClothSize",
                column: "ClothCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClothSize_ClothCategory_ClothCategoryId",
                schema: "dbo",
                table: "ClothSize",
                column: "ClothCategoryId",
                principalSchema: "dbo",
                principalTable: "ClothCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
