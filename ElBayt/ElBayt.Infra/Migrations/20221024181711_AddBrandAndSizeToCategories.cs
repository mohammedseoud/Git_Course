using Microsoft.EntityFrameworkCore.Migrations;

namespace ElBayt.Infra.Migrations
{
    public partial class AddBrandAndSizeToCategories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClothBrands_Cloth_ClothId",
                schema: "dbo",
                table: "ClothBrands");

            migrationBuilder.DropForeignKey(
                name: "FK_ClothSize_Cloth_ClothId",
                schema: "dbo",
                table: "ClothSize");

            migrationBuilder.RenameColumn(
                name: "ClothId",
                schema: "dbo",
                table: "ClothSize",
                newName: "ClothCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_ClothSize_ClothId",
                schema: "dbo",
                table: "ClothSize",
                newName: "IX_ClothSize_ClothCategoryId");

            migrationBuilder.RenameColumn(
                name: "ClothId",
                schema: "dbo",
                table: "ClothBrands",
                newName: "ClothCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_ClothBrands_ClothId",
                schema: "dbo",
                table: "ClothBrands",
                newName: "IX_ClothBrands_ClothCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClothBrands_ClothCategory_ClothCategoryId",
                schema: "dbo",
                table: "ClothBrands",
                column: "ClothCategoryId",
                principalSchema: "dbo",
                principalTable: "ClothCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClothBrands_ClothCategory_ClothCategoryId",
                schema: "dbo",
                table: "ClothBrands");

            migrationBuilder.DropForeignKey(
                name: "FK_ClothSize_ClothCategory_ClothCategoryId",
                schema: "dbo",
                table: "ClothSize");

            migrationBuilder.RenameColumn(
                name: "ClothCategoryId",
                schema: "dbo",
                table: "ClothSize",
                newName: "ClothId");

            migrationBuilder.RenameIndex(
                name: "IX_ClothSize_ClothCategoryId",
                schema: "dbo",
                table: "ClothSize",
                newName: "IX_ClothSize_ClothId");

            migrationBuilder.RenameColumn(
                name: "ClothCategoryId",
                schema: "dbo",
                table: "ClothBrands",
                newName: "ClothId");

            migrationBuilder.RenameIndex(
                name: "IX_ClothBrands_ClothCategoryId",
                schema: "dbo",
                table: "ClothBrands",
                newName: "IX_ClothBrands_ClothId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClothBrands_Cloth_ClothId",
                schema: "dbo",
                table: "ClothBrands",
                column: "ClothId",
                principalSchema: "dbo",
                principalTable: "Cloth",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClothSize_Cloth_ClothId",
                schema: "dbo",
                table: "ClothSize",
                column: "ClothId",
                principalSchema: "dbo",
                principalTable: "Cloth",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
