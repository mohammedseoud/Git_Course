using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ElBayt.Infra.Migrations
{
    public partial class AddClothesToDb14 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cloth",
                schema: "dbo");

            migrationBuilder.AddColumn<string>(
                name: "ProductImageURL3",
                schema: "dbo",
                table: "ProductModel",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductImageURL3",
                schema: "dbo",
                table: "ProductModel");

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
                        principalSchema: "dbo",
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
