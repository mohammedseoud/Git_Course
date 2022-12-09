using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ElBayt.Infra.Migrations
{
    public partial class AddOrderDetailsToDb5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClientId",
                schema: "dbo",
                table: "ClothOrder",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ClothOrderDetails",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    ClothtId = table.Column<int>(type: "int", nullable: false),
                    SizeId = table.Column<int>(type: "int", nullable: false),
                    ColorId = table.Column<int>(type: "int", nullable: false),
                    BrandId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NoteOnProduct = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClothOrderDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClothOrderDetails_Cloth_ClothtId",
                        column: x => x.ClothtId,
                        principalSchema: "dbo",
                        principalTable: "Cloth",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClothOrderDetails_ClothBrand_BrandId",
                        column: x => x.BrandId,
                        principalSchema: "dbo",
                        principalTable: "ClothBrand",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClothOrderDetails_ClothOrder_OrderId",
                        column: x => x.OrderId,
                        principalSchema: "dbo",
                        principalTable: "ClothOrder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClothOrderDetails_ClothSize_SizeId",
                        column: x => x.SizeId,
                        principalSchema: "dbo",
                        principalTable: "ClothSize",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClothOrderDetails_Color_ColorId",
                        column: x => x.ColorId,
                        principalSchema: "dbo",
                        principalTable: "Color",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClothOrder_ClientId",
                schema: "dbo",
                table: "ClothOrder",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ClothOrderDetails_BrandId",
                schema: "dbo",
                table: "ClothOrderDetails",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_ClothOrderDetails_ClothtId",
                schema: "dbo",
                table: "ClothOrderDetails",
                column: "ClothtId");

            migrationBuilder.CreateIndex(
                name: "IX_ClothOrderDetails_ColorId",
                schema: "dbo",
                table: "ClothOrderDetails",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_ClothOrderDetails_OrderId",
                schema: "dbo",
                table: "ClothOrderDetails",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_ClothOrderDetails_SizeId",
                schema: "dbo",
                table: "ClothOrderDetails",
                column: "SizeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClothOrder_Client_ClientId",
                schema: "dbo",
                table: "ClothOrder",
                column: "ClientId",
                principalSchema: "dbo",
                principalTable: "Client",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClothOrder_Client_ClientId",
                schema: "dbo",
                table: "ClothOrder");

            migrationBuilder.DropTable(
                name: "ClothOrderDetails",
                schema: "dbo");

            migrationBuilder.DropIndex(
                name: "IX_ClothOrder_ClientId",
                schema: "dbo",
                table: "ClothOrder");

            migrationBuilder.DropColumn(
                name: "ClientId",
                schema: "dbo",
                table: "ClothOrder");
        }
    }
}
