using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ElBayt.Infra.Migrations
{
    public partial class AddDepartmentTODB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DepartmentId",
                schema: "dbo",
                table: "ProductType",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Department",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Department", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductType_DepartmentId",
                schema: "dbo",
                table: "ProductType",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductType_Department_DepartmentId",
                schema: "dbo",
                table: "ProductType",
                column: "DepartmentId",
                principalSchema: "dbo",
                principalTable: "Department",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductType_Department_DepartmentId",
                schema: "dbo",
                table: "ProductType");

            migrationBuilder.DropTable(
                name: "Department",
                schema: "dbo");

            migrationBuilder.DropIndex(
                name: "IX_ProductType_DepartmentId",
                schema: "dbo",
                table: "ProductType");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                schema: "dbo",
                table: "ProductType");
        }
    }
}
