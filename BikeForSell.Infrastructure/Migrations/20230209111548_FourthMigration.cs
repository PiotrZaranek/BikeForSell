using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BikeForSell.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FourthMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BuyerAddresse_Sales_SalesRef",
                table: "BuyerAddresse");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BuyerAddresse",
                table: "BuyerAddresse");

            migrationBuilder.RenameTable(
                name: "BuyerAddresse",
                newName: "BuyerAddresses");

            migrationBuilder.RenameIndex(
                name: "IX_BuyerAddresse_SalesRef",
                table: "BuyerAddresses",
                newName: "IX_BuyerAddresses_SalesRef");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BuyerAddresses",
                table: "BuyerAddresses",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BuyerAddresses_Sales_SalesRef",
                table: "BuyerAddresses",
                column: "SalesRef",
                principalTable: "Sales",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BuyerAddresses_Sales_SalesRef",
                table: "BuyerAddresses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BuyerAddresses",
                table: "BuyerAddresses");

            migrationBuilder.RenameTable(
                name: "BuyerAddresses",
                newName: "BuyerAddresse");

            migrationBuilder.RenameIndex(
                name: "IX_BuyerAddresses_SalesRef",
                table: "BuyerAddresse",
                newName: "IX_BuyerAddresse_SalesRef");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BuyerAddresse",
                table: "BuyerAddresse",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BuyerAddresse_Sales_SalesRef",
                table: "BuyerAddresse",
                column: "SalesRef",
                principalTable: "Sales",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
