using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BikeForSell.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FifthMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BuyerAddresses");

            migrationBuilder.DropColumn(
                name: "Delivery",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "LastNameBuyer",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "DetailInformations");

            migrationBuilder.RenameColumn(
                name: "SalesManRef",
                table: "Sales",
                newName: "SalesmanRef");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "Sales",
                newName: "Prize");

            migrationBuilder.RenameColumn(
                name: "NameBuyer",
                table: "Sales",
                newName: "Name");

            migrationBuilder.AddColumn<int>(
                name: "BuyerRef",
                table: "Sales",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Sales",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Bikes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsBought",
                table: "Bikes",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BuyerRef",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Bikes");

            migrationBuilder.DropColumn(
                name: "IsBought",
                table: "Bikes");

            migrationBuilder.RenameColumn(
                name: "SalesmanRef",
                table: "Sales",
                newName: "SalesManRef");

            migrationBuilder.RenameColumn(
                name: "Prize",
                table: "Sales",
                newName: "PhoneNumber");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Sales",
                newName: "NameBuyer");

            migrationBuilder.AddColumn<bool>(
                name: "Delivery",
                table: "Sales",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Sales",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastNameBuyer",
                table: "Sales",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "DetailInformations",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "BuyerAddresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SalesRef = table.Column<int>(type: "int", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ZipCode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuyerAddresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BuyerAddresses_Sales_SalesRef",
                        column: x => x.SalesRef,
                        principalTable: "Sales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BuyerAddresses_SalesRef",
                table: "BuyerAddresses",
                column: "SalesRef",
                unique: true);
        }
    }
}
