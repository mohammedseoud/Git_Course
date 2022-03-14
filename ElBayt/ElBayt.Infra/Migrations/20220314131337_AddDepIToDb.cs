using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ElBayt.Infra.Migrations
{
    public partial class AddDepIToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ClothDepartmentId",
                schema: "dbo",
                table: "ClothType",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_ClothType_ClothDepartmentId",
                schema: "dbo",
                table: "ClothType",
                column: "ClothDepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClothType_ClothDepartment_ClothDepartmentId",
                schema: "dbo",
                table: "ClothType",
                column: "ClothDepartmentId",
                principalSchema: "dbo",
                principalTable: "ClothDepartment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClothType_ClothDepartment_ClothDepartmentId",
                schema: "dbo",
                table: "ClothType");

            migrationBuilder.DropIndex(
                name: "IX_ClothType_ClothDepartmentId",
                schema: "dbo",
                table: "ClothType");

            migrationBuilder.DropColumn(
                name: "ClothDepartmentId",
                schema: "dbo",
                table: "ClothType");
        }
    }
}
