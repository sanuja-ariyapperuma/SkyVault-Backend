using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SkyVault.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class AddNationalityCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "nationality_code",
                table: "nationalities",
                type: "char(3)",
                fixedLength: true,
                maxLength: 3,
                nullable: false,
                defaultValue: "",
                collation: "utf8mb4_0900_ai_ci")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 1,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 2,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 3,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 4,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 5,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 6,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 7,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 8,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 9,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 10,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 11,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 12,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 13,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 14,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 15,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 16,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 17,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 18,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 19,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 20,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 21,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 22,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 23,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 24,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 25,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 26,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 27,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 28,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 29,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 30,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 31,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 32,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 33,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 34,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 35,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 36,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 37,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 38,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 39,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 40,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 41,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 42,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 43,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 44,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 45,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 46,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 47,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 48,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 49,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 50,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 51,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 52,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 53,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 54,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 55,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 56,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 57,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 58,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 59,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 60,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 61,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 62,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 63,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 64,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 65,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 66,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 67,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 68,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 69,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 70,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 71,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 72,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 73,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 74,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 75,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 76,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 77,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 78,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 79,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 80,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 81,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 82,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 83,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 84,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 85,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 86,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 87,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 88,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 89,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 90,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 91,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 92,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 93,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 94,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 95,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 96,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 97,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 98,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 99,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 100,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 101,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 102,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 103,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 104,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 105,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 106,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 107,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 108,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 109,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 110,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 111,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 112,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 113,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 114,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 115,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 116,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 117,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 118,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 119,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 120,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 121,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 122,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 123,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 124,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 125,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 126,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 127,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 128,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 129,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 130,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 131,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 132,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 133,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 134,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 135,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 136,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 137,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 138,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 139,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 140,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 141,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 142,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 143,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 144,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 145,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 146,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 147,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 148,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 149,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 150,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 151,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 152,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 153,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 154,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 155,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 156,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 157,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 158,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 159,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 160,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 161,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 162,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 163,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 164,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 165,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 166,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 167,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 168,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 169,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 170,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 171,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 172,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 173,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 174,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 175,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 176,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 177,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 178,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 179,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 180,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 181,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 182,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 183,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 184,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 185,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 186,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 187,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 188,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 189,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 190,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 191,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 192,
                column: "nationality_code",
                value: "");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 193,
                column: "nationality_code",
                value: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "nationality_code",
                table: "nationalities");
        }
    }
}
