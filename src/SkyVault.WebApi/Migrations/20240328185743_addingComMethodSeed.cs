using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SkyVault.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class addingComMethodSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "communication_methods",
                columns: new[] { "id", "comm_title" },
                values: new object[,]
                {
                    { 1, "Non" },
                    { 2, "Email" },
                    { 3, "WhatsApp" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "communication_methods",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "communication_methods",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "communication_methods",
                keyColumn: "id",
                keyValue: 3);
        }
    }
}
