using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ElBayt.Infra.Migrations
{
    public partial class AddClothesToDb5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClothDepartment",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClothDepartment", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ClothType",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClothTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClothType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClothType_ClothDepartment_ClothTypeId",
                        column: x => x.ClothTypeId,
                        principalSchema: "dbo",
                        principalTable: "ClothDepartment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClothCategory",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClothTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClothCategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClothCategory_ClothType_ClothTypeId",
                        column: x => x.ClothTypeId,
                        principalSchema: "dbo",
                        principalTable: "ClothType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cloth",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClothCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cloth", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cloth_ClothCategory_ClothCategoryId",
                        column: x => x.ClothCategoryId,
                        principalSchema: "dbo",
                        principalTable: "ClothCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cloth_ProductModel_Id",
                        column: x => x.Id,
                        principalTable: "ProductModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cloth_ClothCategoryId",
                schema: "dbo",
                table: "Cloth",
                column: "ClothCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ClothCategory_ClothTypeId",
                schema: "dbo",
                table: "ClothCategory",
                column: "ClothTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ClothType_ClothTypeId",
                schema: "dbo",
                table: "ClothType",
                column: "ClothTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cloth",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "ClothCategory",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "ClothType",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "ClothDepartment",
                schema: "dbo");
        }
    }
}
