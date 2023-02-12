using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BikeForSell.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EighthMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PurchaseRef",
                table: "Sales",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Sales_PurchaseRef",
                table: "Sales",
                column: "PurchaseRef",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_Purchases_PurchaseRef",
                table: "Sales",
                column: "PurchaseRef",
                principalTable: "Purchases",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sales_Purchases_PurchaseRef",
                table: "Sales");

            migrationBuilder.DropIndex(
                name: "IX_Sales_PurchaseRef",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "PurchaseRef",
                table: "Sales");
        }
    }
}
