using Microsoft.EntityFrameworkCore.Migrations;

namespace ElBayt.Infra.Migrations
{
    public partial class AddOrderDetailsToDb2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SecondAppartmentAddress",
                schema: "dbo",
                table: "ClientInfo",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SecondCityId",
                schema: "dbo",
                table: "ClientInfo",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "SecondCompanyName",
                schema: "dbo",
                table: "ClientInfo",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SecondFirstName",
                schema: "dbo",
                table: "ClientInfo",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SecondLastName",
                schema: "dbo",
                table: "ClientInfo",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SecondPostcode",
                schema: "dbo",
                table: "ClientInfo",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SecondStreetAddress",
                schema: "dbo",
                table: "ClientInfo",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClientInfo_SecondCityId",
                schema: "dbo",
                table: "ClientInfo",
                column: "SecondCityId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClientInfo_City_SecondCityId",
                schema: "dbo",
                table: "ClientInfo",
                column: "SecondCityId",
                principalSchema: "dbo",
                principalTable: "City",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClientInfo_City_SecondCityId",
                schema: "dbo",
                table: "ClientInfo");

            migrationBuilder.DropIndex(
                name: "IX_ClientInfo_SecondCityId",
                schema: "dbo",
                table: "ClientInfo");

            migrationBuilder.DropColumn(
                name: "SecondAppartmentAddress",
                schema: "dbo",
                table: "ClientInfo");

            migrationBuilder.DropColumn(
                name: "SecondCityId",
                schema: "dbo",
                table: "ClientInfo");

            migrationBuilder.DropColumn(
                name: "SecondCompanyName",
                schema: "dbo",
                table: "ClientInfo");

            migrationBuilder.DropColumn(
                name: "SecondFirstName",
                schema: "dbo",
                table: "ClientInfo");

            migrationBuilder.DropColumn(
                name: "SecondLastName",
                schema: "dbo",
                table: "ClientInfo");

            migrationBuilder.DropColumn(
                name: "SecondPostcode",
                schema: "dbo",
                table: "ClientInfo");

            migrationBuilder.DropColumn(
                name: "SecondStreetAddress",
                schema: "dbo",
                table: "ClientInfo");
        }
    }
}
