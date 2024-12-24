using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SkyVault.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class AddNewColumnToVisaDestination : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DestinationCountryId",
                table: "visas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_visas_DestinationCountryId",
                table: "visas",
                column: "DestinationCountryId");

            migrationBuilder.AddForeignKey(
                name: "FK_visas_countries_DestinationCountryId",
                table: "visas",
                column: "DestinationCountryId",
                principalTable: "countries",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_visas_countries_DestinationCountryId",
                table: "visas");

            migrationBuilder.DropIndex(
                name: "IX_visas_DestinationCountryId",
                table: "visas");

            migrationBuilder.DropColumn(
                name: "DestinationCountryId",
                table: "visas");
        }
    }
}
