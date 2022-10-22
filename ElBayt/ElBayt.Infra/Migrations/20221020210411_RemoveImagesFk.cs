using Microsoft.EntityFrameworkCore.Migrations;

namespace ElBayt.Infra.Migrations
{
    public partial class RemoveImagesFk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClothImage_Cloth_ClothId",
                schema: "dbo",
                table: "ClothImage");

            migrationBuilder.DropIndex(
                name: "IX_ClothImage_ClothId",
                schema: "dbo",
                table: "ClothImage");

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
                name: "FK_ClothImage_Cloth_ClothsId",
                schema: "dbo",
                table: "ClothImage");

            migrationBuilder.DropIndex(
                name: "IX_ClothImage_ClothsId",
                schema: "dbo",
                table: "ClothImage");

            migrationBuilder.DropColumn(
                name: "ClothsId",
                schema: "dbo",
                table: "ClothImage");

            migrationBuilder.CreateIndex(
                name: "IX_ClothImage_ClothId",
                schema: "dbo",
                table: "ClothImage",
                column: "ClothId");

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
