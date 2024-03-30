using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SkyVault.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class AddingExpiryDateToPassport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateOnly>(
                name: "ExpiryDate",
                table: "passports",
                type: "date",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpiryDate",
                table: "passports");
        }
    }
}
