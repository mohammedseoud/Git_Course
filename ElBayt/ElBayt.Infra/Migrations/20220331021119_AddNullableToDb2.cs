using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ElBayt.Infra.Migrations
{
    public partial class AddNullableToDb2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClothInfo_ClothBrand_BrandId",
                schema: "dbo",
                table: "ClothInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_ClothInfo_Color_ColorId",
                schema: "dbo",
                table: "ClothInfo");

            migrationBuilder.AlterColumn<Guid>(
                name: "ColorId",
                schema: "dbo",
                table: "ClothInfo",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "BrandId",
                schema: "dbo",
                table: "ClothInfo",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_ClothInfo_ClothBrand_BrandId",
                schema: "dbo",
                table: "ClothInfo",
                column: "BrandId",
                principalSchema: "dbo",
                principalTable: "ClothBrand",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_ClothInfo_Color_ColorId",
                schema: "dbo",
                table: "ClothInfo",
                column: "ColorId",
                principalSchema: "dbo",
                principalTable: "Color",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClothInfo_ClothBrand_BrandId",
                schema: "dbo",
                table: "ClothInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_ClothInfo_Color_ColorId",
                schema: "dbo",
                table: "ClothInfo");

            migrationBuilder.AlterColumn<Guid>(
                name: "ColorId",
                schema: "dbo",
                table: "ClothInfo",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "BrandId",
                schema: "dbo",
                table: "ClothInfo",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ClothInfo_ClothBrand_BrandId",
                schema: "dbo",
                table: "ClothInfo",
                column: "BrandId",
                principalSchema: "dbo",
                principalTable: "ClothBrand",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClothInfo_Color_ColorId",
                schema: "dbo",
                table: "ClothInfo",
                column: "ColorId",
                principalSchema: "dbo",
                principalTable: "Color",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
