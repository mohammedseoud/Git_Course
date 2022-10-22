using Microsoft.EntityFrameworkCore.Migrations;

namespace ElBayt.Infra.Migrations
{
    public partial class RemoveImagesFk1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cloth_ClothCategory_ClothCategoryId",
                schema: "dbo",
                table: "Cloth");

            migrationBuilder.DropIndex(
                name: "IX_Cloth_ClothCategoryId",
                schema: "dbo",
                table: "Cloth");

            migrationBuilder.AddColumn<int>(
                name: "ClothCategoriesId",
                schema: "dbo",
                table: "Cloth",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cloth_ClothCategoriesId",
                schema: "dbo",
                table: "Cloth",
                column: "ClothCategoriesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cloth_ClothCategory_ClothCategoriesId",
                schema: "dbo",
                table: "Cloth",
                column: "ClothCategoriesId",
                principalSchema: "dbo",
                principalTable: "ClothCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cloth_ClothCategory_ClothCategoriesId",
                schema: "dbo",
                table: "Cloth");

            migrationBuilder.DropIndex(
                name: "IX_Cloth_ClothCategoriesId",
                schema: "dbo",
                table: "Cloth");

            migrationBuilder.DropColumn(
                name: "ClothCategoriesId",
                schema: "dbo",
                table: "Cloth");

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
    }
}
