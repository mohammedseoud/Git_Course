using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ElBayt.Infra.Migrations
{
    public partial class AddClothIdForSizeToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ClothId",
                schema: "dbo",
                table: "ClothSize",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_ClothSize_ClothId",
                schema: "dbo",
                table: "ClothSize",
                column: "ClothId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClothSize_Cloth_ClothId",
                schema: "dbo",
                table: "ClothSize",
                column: "ClothId",
                principalSchema: "dbo",
                principalTable: "Cloth",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClothSize_Cloth_ClothId",
                schema: "dbo",
                table: "ClothSize");

            migrationBuilder.DropIndex(
                name: "IX_ClothSize_ClothId",
                schema: "dbo",
                table: "ClothSize");

            migrationBuilder.DropColumn(
                name: "ClothId",
                schema: "dbo",
                table: "ClothSize");
        }
    }
}
