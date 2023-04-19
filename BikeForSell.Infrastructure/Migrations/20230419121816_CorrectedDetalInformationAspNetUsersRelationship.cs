using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BikeForSell.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CorrectedDetalInformationAspNetUsersRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetailInformations_AspNetUsers_UserRef",
                table: "DetailInformations");

            migrationBuilder.DropIndex(
                name: "IX_DetailInformations_UserRef",
                table: "DetailInformations");

            migrationBuilder.AlterColumn<string>(
                name: "UserRef",
                table: "DetailInformations",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "DetailInformations",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DetailInformations_UserId",
                table: "DetailInformations",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_DetailInformations_AspNetUsers_UserId",
                table: "DetailInformations",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetailInformations_AspNetUsers_UserId",
                table: "DetailInformations");

            migrationBuilder.DropIndex(
                name: "IX_DetailInformations_UserId",
                table: "DetailInformations");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "DetailInformations");

            migrationBuilder.AlterColumn<string>(
                name: "UserRef",
                table: "DetailInformations",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_DetailInformations_UserRef",
                table: "DetailInformations",
                column: "UserRef",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DetailInformations_AspNetUsers_UserRef",
                table: "DetailInformations",
                column: "UserRef",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
