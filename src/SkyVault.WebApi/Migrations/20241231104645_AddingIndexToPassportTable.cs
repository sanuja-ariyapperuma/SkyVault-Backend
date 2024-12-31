using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SkyVault.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class AddingIndexToPassportTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "idx_passports_last_name",
                table: "passports",
                column: "last_name");

            migrationBuilder.CreateIndex(
                name: "idx_passports_other_names",
                table: "passports",
                column: "other_names");

            migrationBuilder.CreateIndex(
                name: "idx_passports_passport_number",
                table: "passports",
                column: "passport_number");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "idx_passports_last_name",
                table: "passports");

            migrationBuilder.DropIndex(
                name: "idx_passports_other_names",
                table: "passports");

            migrationBuilder.DropIndex(
                name: "idx_passports_passport_number",
                table: "passports");
        }
    }
}
