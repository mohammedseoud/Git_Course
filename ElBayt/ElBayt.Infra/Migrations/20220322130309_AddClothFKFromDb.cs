using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ElBayt.Infra.Migrations
{
    public partial class AddClothFKFromDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ClothCategoryId",
                schema: "dbo",
                table: "Cloth",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Cloth_ClothCategoryId",
                schema: "dbo",
                table: "Cloth",
                column: "ClothCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cloth_ClothCategory_ClothCategoryId",
                schema: "dbo",
                table: "Cloth",
                column: "ClothCategoryId",
                principalSchema: "dbo",
                principalTable: "ClothCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cloth_ClothCategory_ClothCategoryId",
                schema: "dbo",
                table: "Cloth");

            migrationBuilder.DropIndex(
                name: "IX_Cloth_ClothCategoryId",
                schema: "dbo",
                table: "Cloth");

            migrationBuilder.DropColumn(
                name: "ClothCategoryId",
                schema: "dbo",
                table: "Cloth");
        }
    }
}
