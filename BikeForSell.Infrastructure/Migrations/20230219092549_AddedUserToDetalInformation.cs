using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BikeForSell.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedUserToDetalInformation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "DetailInformations");

            migrationBuilder.DropColumn(
                name: "LastNameSalesman",
                table: "DetailInformations");

            migrationBuilder.DropColumn(
                name: "NameSalesman",
                table: "DetailInformations");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "Email",
                table: "DetailInformations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastNameSalesman",
                table: "DetailInformations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NameSalesman",
                table: "DetailInformations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "DetailInformations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
