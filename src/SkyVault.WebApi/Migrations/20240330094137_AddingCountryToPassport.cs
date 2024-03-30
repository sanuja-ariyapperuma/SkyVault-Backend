using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SkyVault.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class AddingCountryToPassport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CountryId",
                table: "passports",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_passports_CountryId",
                table: "passports",
                column: "CountryId");

            migrationBuilder.AddForeignKey(
                name: "FK_passports_countries_CountryId",
                table: "passports",
                column: "CountryId",
                principalTable: "countries",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_passports_countries_CountryId",
                table: "passports");

            migrationBuilder.DropIndex(
                name: "IX_passports_CountryId",
                table: "passports");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "passports");
        }
    }
}
