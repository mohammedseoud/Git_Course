using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ElBayt.Infra.Migrations
{
    public partial class RemoveClothesDepartmentFromDb17 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClothType_ClothDepartment_ClothTypeId",
                schema: "dbo",
                table: "ClothType");

            migrationBuilder.DropTable(
                name: "ClothDepartment",
                schema: "dbo");

            migrationBuilder.DropIndex(
                name: "IX_ClothType_ClothTypeId",
                schema: "dbo",
                table: "ClothType");

            migrationBuilder.DropColumn(
                name: "ClothTypeId",
                schema: "dbo",
                table: "ClothType");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ClothTypeId",
                schema: "dbo",
                table: "ClothType",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "ClothDepartment",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClothDepartment", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClothType_ClothTypeId",
                schema: "dbo",
                table: "ClothType",
                column: "ClothTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClothType_ClothDepartment_ClothTypeId",
                schema: "dbo",
                table: "ClothType",
                column: "ClothTypeId",
                principalSchema: "dbo",
                principalTable: "ClothDepartment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
