using Microsoft.EntityFrameworkCore.Migrations;

namespace ElBayt.Infra.Migrations
{
    public partial class RemoveFks4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClothInfo_Cloth_ClothId",
                schema: "dbo",
                table: "ClothInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_ClothSize_Cloth_ClothId",
                schema: "dbo",
                table: "ClothSize");

            migrationBuilder.DropIndex(
                name: "IX_ClothSize_ClothId",
                schema: "dbo",
                table: "ClothSize");

            migrationBuilder.DropIndex(
                name: "IX_ClothInfo_ClothId",
                schema: "dbo",
                table: "ClothInfo");

            migrationBuilder.AddColumn<int>(
                name: "ClothesId",
                schema: "dbo",
                table: "ClothSize",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ClothesId",
                schema: "dbo",
                table: "ClothInfo",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClothSize_ClothesId",
                schema: "dbo",
                table: "ClothSize",
                column: "ClothesId");

            migrationBuilder.CreateIndex(
                name: "IX_ClothInfo_ClothesId",
                schema: "dbo",
                table: "ClothInfo",
                column: "ClothesId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClothInfo_Cloth_ClothesId",
                schema: "dbo",
                table: "ClothInfo",
                column: "ClothesId",
                principalSchema: "dbo",
                principalTable: "Cloth",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ClothSize_Cloth_ClothesId",
                schema: "dbo",
                table: "ClothSize",
                column: "ClothesId",
                principalSchema: "dbo",
                principalTable: "Cloth",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClothInfo_Cloth_ClothesId",
                schema: "dbo",
                table: "ClothInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_ClothSize_Cloth_ClothesId",
                schema: "dbo",
                table: "ClothSize");

            migrationBuilder.DropIndex(
                name: "IX_ClothSize_ClothesId",
                schema: "dbo",
                table: "ClothSize");

            migrationBuilder.DropIndex(
                name: "IX_ClothInfo_ClothesId",
                schema: "dbo",
                table: "ClothInfo");

            migrationBuilder.DropColumn(
                name: "ClothesId",
                schema: "dbo",
                table: "ClothSize");

            migrationBuilder.DropColumn(
                name: "ClothesId",
                schema: "dbo",
                table: "ClothInfo");

            migrationBuilder.CreateIndex(
                name: "IX_ClothSize_ClothId",
                schema: "dbo",
                table: "ClothSize",
                column: "ClothId");

            migrationBuilder.CreateIndex(
                name: "IX_ClothInfo_ClothId",
                schema: "dbo",
                table: "ClothInfo",
                column: "ClothId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClothInfo_Cloth_ClothId",
                schema: "dbo",
                table: "ClothInfo",
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
