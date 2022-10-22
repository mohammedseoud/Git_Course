using Microsoft.EntityFrameworkCore.Migrations;

namespace ElBayt.Infra.Migrations
{
    public partial class RemoveImagesFk2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cloth_ClothCategory_ClothCategoriesId",
                schema: "dbo",
                table: "Cloth");

            migrationBuilder.DropForeignKey(
                name: "FK_ClothImage_Cloth_ClothId",
                schema: "dbo",
                table: "ClothImage");

            migrationBuilder.DropIndex(
                name: "IX_ClothImage_ClothId",
                schema: "dbo",
                table: "ClothImage");

            migrationBuilder.DropIndex(
                name: "IX_Cloth_ClothCategoriesId",
                schema: "dbo",
                table: "Cloth");

            migrationBuilder.DropColumn(
                name: "ClothCategoriesId",
                schema: "dbo",
                table: "Cloth");

            migrationBuilder.AddColumn<int>(
                name: "ClothsId",
                schema: "dbo",
                table: "ClothImage",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClothImage_ClothsId",
                schema: "dbo",
                table: "ClothImage",
                column: "ClothsId");

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
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ClothImage_Cloth_ClothsId",
                schema: "dbo",
                table: "ClothImage",
                column: "ClothsId",
                principalSchema: "dbo",
                principalTable: "Cloth",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cloth_ClothCategory_ClothCategoryId",
                schema: "dbo",
                table: "Cloth");

            migrationBuilder.DropForeignKey(
                name: "FK_ClothImage_Cloth_ClothsId",
                schema: "dbo",
                table: "ClothImage");

            migrationBuilder.DropIndex(
                name: "IX_ClothImage_ClothsId",
                schema: "dbo",
                table: "ClothImage");

            migrationBuilder.DropIndex(
                name: "IX_Cloth_ClothCategoryId",
                schema: "dbo",
                table: "Cloth");

            migrationBuilder.DropColumn(
                name: "ClothsId",
                schema: "dbo",
                table: "ClothImage");

            migrationBuilder.AddColumn<int>(
                name: "ClothCategoriesId",
                schema: "dbo",
                table: "Cloth",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClothImage_ClothId",
                schema: "dbo",
                table: "ClothImage",
                column: "ClothId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_ClothImage_Cloth_ClothId",
                schema: "dbo",
                table: "ClothImage",
                column: "ClothId",
                principalSchema: "dbo",
                principalTable: "Cloth",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
