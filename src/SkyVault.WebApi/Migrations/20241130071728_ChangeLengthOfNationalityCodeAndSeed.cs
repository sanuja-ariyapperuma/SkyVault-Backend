using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SkyVault.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class ChangeLengthOfNationalityCodeAndSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "nationality_code",
                table: "nationalities",
                type: "char(6)",
                fixedLength: true,
                maxLength: 6,
                nullable: false,
                collation: "utf8mb4_0900_ai_ci",
                oldClrType: typeof(string),
                oldType: "char(3)",
                oldFixedLength: true,
                oldMaxLength: 3)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 1,
                column: "nationality_code",
                value: "AFG");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 2,
                column: "nationality_code",
                value: "ALB");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 3,
                column: "nationality_code",
                value: "DZA");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 4,
                column: "nationality_code",
                value: "USA");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 5,
                column: "nationality_code",
                value: "AND");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 6,
                column: "nationality_code",
                value: "AGO");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 7,
                column: "nationality_code",
                value: "ATG");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 8,
                column: "nationality_code",
                value: "ARG");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 9,
                column: "nationality_code",
                value: "ARM");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 10,
                column: "nationality_code",
                value: "AUS");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 11,
                column: "nationality_code",
                value: "AUT");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 12,
                column: "nationality_code",
                value: "AZE");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 13,
                column: "nationality_code",
                value: "BHS");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 14,
                column: "nationality_code",
                value: "BHR");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 15,
                column: "nationality_code",
                value: "BGD");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 16,
                column: "nationality_code",
                value: "BRB");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 17,
                column: "nationality_code",
                value: "BLR");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 18,
                column: "nationality_code",
                value: "BEL");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 19,
                column: "nationality_code",
                value: "BLZ");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 20,
                column: "nationality_code",
                value: "BEN");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 21,
                column: "nationality_code",
                value: "BTN");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 22,
                column: "nationality_code",
                value: "BOL");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 23,
                column: "nationality_code",
                value: "BIH");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 24,
                column: "nationality_code",
                value: "BWA");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 25,
                column: "nationality_code",
                value: "BRA");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 26,
                column: "nationality_code",
                value: "GBR");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 27,
                column: "nationality_code",
                value: "BRN");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 28,
                column: "nationality_code",
                value: "BGR");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 29,
                column: "nationality_code",
                value: "BFA");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 30,
                column: "nationality_code",
                value: "BDI");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 31,
                column: "nationality_code",
                value: "KHM");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 32,
                column: "nationality_code",
                value: "CMR");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 33,
                column: "nationality_code",
                value: "CAN");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 34,
                column: "nationality_code",
                value: "CPV");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 35,
                column: "nationality_code",
                value: "CAF");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 36,
                column: "nationality_code",
                value: "TCD");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 37,
                column: "nationality_code",
                value: "CHL");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 38,
                column: "nationality_code",
                value: "CHN");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 39,
                column: "nationality_code",
                value: "COL");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 40,
                column: "nationality_code",
                value: "COM");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 41,
                column: "nationality_code",
                value: "COG");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 42,
                column: "nationality_code",
                value: "CRI");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 43,
                column: "nationality_code",
                value: "HRV");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 44,
                column: "nationality_code",
                value: "CUB");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 45,
                column: "nationality_code",
                value: "CYP");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 46,
                column: "nationality_code",
                value: "CZE");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 47,
                column: "nationality_code",
                value: "DNK");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 48,
                column: "nationality_code",
                value: "DJI");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 49,
                column: "nationality_code",
                value: "DOM");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 50,
                column: "nationality_code",
                value: "NLD");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 51,
                column: "nationality_code",
                value: "TLS");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 52,
                column: "nationality_code",
                value: "ECU");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 53,
                column: "nationality_code",
                value: "EGY");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 54,
                column: "nationality_code",
                value: "ARE");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 55,
                column: "nationality_code",
                value: "GNQ");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 56,
                column: "nationality_code",
                value: "ERI");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 57,
                column: "nationality_code",
                value: "EST");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 58,
                column: "nationality_code",
                value: "ETH");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 59,
                column: "nationality_code",
                value: "FJI");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 60,
                column: "nationality_code",
                value: "PHL");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 61,
                column: "nationality_code",
                value: "FIN");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 62,
                column: "nationality_code",
                value: "FRA");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 63,
                column: "nationality_code",
                value: "GAB");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 64,
                column: "nationality_code",
                value: "GMB");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 65,
                column: "nationality_code",
                value: "GEO");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 66,
                column: "nationality_code",
                value: "DEU");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 67,
                column: "nationality_code",
                value: "GHA");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 68,
                column: "nationality_code",
                value: "GRC");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 69,
                column: "nationality_code",
                value: "GRD");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 70,
                column: "nationality_code",
                value: "GTM");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 71,
                column: "nationality_code",
                value: "GNB");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 72,
                column: "nationality_code",
                value: "GIN");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 73,
                column: "nationality_code",
                value: "GUY");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 74,
                column: "nationality_code",
                value: "HTI");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 75,
                column: "nationality_code",
                value: "BIH");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 76,
                column: "nationality_code",
                value: "HND");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 77,
                column: "nationality_code",
                value: "HUN");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 78,
                column: "nationality_code",
                value: "KIR");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 79,
                column: "nationality_code",
                value: "ISL");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 80,
                column: "nationality_code",
                value: "IND");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 81,
                column: "nationality_code",
                value: "IDN");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 82,
                column: "nationality_code",
                value: "IRN");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 83,
                column: "nationality_code",
                value: "IRQ");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 84,
                column: "nationality_code",
                value: "IRL");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 85,
                column: "nationality_code",
                value: "ISR");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 86,
                column: "nationality_code",
                value: "ITA");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 87,
                column: "nationality_code",
                value: "CIV");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 88,
                column: "nationality_code",
                value: "JAM");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 89,
                column: "nationality_code",
                value: "JPN");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 90,
                column: "nationality_code",
                value: "JOR");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 91,
                column: "nationality_code",
                value: "KAZ");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 92,
                column: "nationality_code",
                value: "KEN");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 93,
                column: "nationality_code",
                value: "KNA");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 94,
                column: "nationality_code",
                value: "KWT");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 95,
                column: "nationality_code",
                value: "KGZ");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 96,
                column: "nationality_code",
                value: "LAO");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 97,
                column: "nationality_code",
                value: "LVA");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 98,
                column: "nationality_code",
                value: "LBN");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 99,
                column: "nationality_code",
                value: "LBR");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 100,
                column: "nationality_code",
                value: "LBY");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 101,
                column: "nationality_code",
                value: "LIE");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 102,
                column: "nationality_code",
                value: "LTU");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 103,
                column: "nationality_code",
                value: "LUX");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 104,
                column: "nationality_code",
                value: "MDG");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 105,
                column: "nationality_code",
                value: "MWI");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 106,
                column: "nationality_code",
                value: "MYS");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 107,
                column: "nationality_code",
                value: "MDV");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 108,
                column: "nationality_code",
                value: "MLI");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 109,
                column: "nationality_code",
                value: "MLT");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 110,
                column: "nationality_code",
                value: "MHL");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 111,
                column: "nationality_code",
                value: "MRT");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 112,
                column: "nationality_code",
                value: "MUS");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 113,
                column: "nationality_code",
                value: "MEX");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 114,
                column: "nationality_code",
                value: "FSM");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 115,
                column: "nationality_code",
                value: "MDA");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 116,
                column: "nationality_code",
                value: "MCO");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 117,
                column: "nationality_code",
                value: "MNG");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 118,
                column: "nationality_code",
                value: "MNE");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 119,
                column: "nationality_code",
                value: "MAR");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 120,
                column: "nationality_code",
                value: "LSO");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 121,
                column: "nationality_code",
                value: "BWA");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 122,
                column: "nationality_code",
                value: "MOZ");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 123,
                column: "nationality_code",
                value: "NAM");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 124,
                column: "nationality_code",
                value: "NRU");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 125,
                column: "nationality_code",
                value: "NPL");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 126,
                column: "nationality_code",
                value: "NLD");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 127,
                column: "nationality_code",
                value: "NZL");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 128,
                column: "nationality_code",
                value: "NIC");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 129,
                column: "nationality_code",
                value: "NGA");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 130,
                column: "nationality_code",
                value: "PRK");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 131,
                column: "nationality_code",
                value: "GB-NIR");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 132,
                column: "nationality_code",
                value: "NOR");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 133,
                column: "nationality_code",
                value: "OMN");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 134,
                column: "nationality_code",
                value: "PAK");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 135,
                column: "nationality_code",
                value: "PLW");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 136,
                column: "nationality_code",
                value: "PAN");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 137,
                column: "nationality_code",
                value: "PNG");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 138,
                column: "nationality_code",
                value: "PRY");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 139,
                column: "nationality_code",
                value: "PER");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 140,
                column: "nationality_code",
                value: "POL");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 141,
                column: "nationality_code",
                value: "PRT");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 142,
                column: "nationality_code",
                value: "QAT");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 143,
                column: "nationality_code",
                value: "ROU");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 144,
                column: "nationality_code",
                value: "RUS");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 145,
                column: "nationality_code",
                value: "RWA");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 146,
                columns: new[] { "nationality_code", "nationality_name" },
                values: new object[] { "KNA", "Saint Kitts and Nevis" });

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 147,
                columns: new[] { "nationality_code", "nationality_name" },
                values: new object[] { "LCA", "Saint Lucian" });

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 148,
                columns: new[] { "nationality_code", "nationality_name" },
                values: new object[] { "VCT", "Saint Vincent and Grenadines" });

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 149,
                columns: new[] { "nationality_code", "nationality_name" },
                values: new object[] { "WSM", "Samoan" });

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 150,
                columns: new[] { "nationality_code", "nationality_name" },
                values: new object[] { "SMR", "San Marinese" });

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 151,
                columns: new[] { "nationality_code", "nationality_name" },
                values: new object[] { "STP", "Sao Tomean" });

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 152,
                columns: new[] { "nationality_code", "nationality_name" },
                values: new object[] { "SAU", "Saudi" });

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 153,
                columns: new[] { "nationality_code", "nationality_name" },
                values: new object[] { "GB-SCT", "Scottish" });

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 154,
                columns: new[] { "nationality_code", "nationality_name" },
                values: new object[] { "SEN", "Senegalese" });

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 155,
                columns: new[] { "nationality_code", "nationality_name" },
                values: new object[] { "SRB", "Serbian" });

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 156,
                columns: new[] { "nationality_code", "nationality_name" },
                values: new object[] { "SYC", "Seychellois" });

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 157,
                columns: new[] { "nationality_code", "nationality_name" },
                values: new object[] { "SLE", "Sierra Leonean" });

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 158,
                columns: new[] { "nationality_code", "nationality_name" },
                values: new object[] { "SGP", "Singaporean" });

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 159,
                columns: new[] { "nationality_code", "nationality_name" },
                values: new object[] { "SVK", "Slovak" });

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 160,
                columns: new[] { "nationality_code", "nationality_name" },
                values: new object[] { "SVN", "Slovene" });

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 161,
                columns: new[] { "nationality_code", "nationality_name" },
                values: new object[] { "SLB", "Solomon Islander" });

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 162,
                columns: new[] { "nationality_code", "nationality_name" },
                values: new object[] { "SOM", "Somali" });

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 163,
                columns: new[] { "nationality_code", "nationality_name" },
                values: new object[] { "ZAF", "South African" });

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 164,
                columns: new[] { "nationality_code", "nationality_name" },
                values: new object[] { "KOR", "South Korean" });

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 165,
                column: "nationality_code",
                value: "ESP");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 166,
                column: "nationality_code",
                value: "LKA");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 167,
                column: "nationality_code",
                value: "SDN");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 168,
                columns: new[] { "nationality_code", "nationality_name" },
                values: new object[] { "SUR", "Surinamese" });

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 169,
                column: "nationality_code",
                value: "SWZ");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 170,
                column: "nationality_code",
                value: "SWE");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 171,
                column: "nationality_code",
                value: "CHE");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 172,
                column: "nationality_code",
                value: "SYR");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 173,
                column: "nationality_code",
                value: "TWN");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 174,
                column: "nationality_code",
                value: "TJK");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 175,
                column: "nationality_code",
                value: "TZA");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 176,
                column: "nationality_code",
                value: "THA");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 177,
                column: "nationality_code",
                value: "TGO");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 178,
                column: "nationality_code",
                value: "TON");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 179,
                column: "nationality_code",
                value: "TTO");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 180,
                column: "nationality_code",
                value: "TUN");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 181,
                column: "nationality_code",
                value: "TUR");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 182,
                columns: new[] { "nationality_code", "nationality_name" },
                values: new object[] { "TUV", "Tuvaluan" });

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 183,
                columns: new[] { "nationality_code", "nationality_name" },
                values: new object[] { "UGA", "Ugandan" });

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 184,
                columns: new[] { "nationality_code", "nationality_name" },
                values: new object[] { "UKR", "Ukrainian" });

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 185,
                columns: new[] { "nationality_code", "nationality_name" },
                values: new object[] { "URY", "Uruguayan" });

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 186,
                columns: new[] { "nationality_code", "nationality_name" },
                values: new object[] { "UZB", "Uzbek" });

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 187,
                columns: new[] { "nationality_code", "nationality_name" },
                values: new object[] { "VUT", "Vanuatu" });

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 188,
                columns: new[] { "nationality_code", "nationality_name" },
                values: new object[] { "VAT", "Vatican" });

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 189,
                column: "nationality_code",
                value: "VEN");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 190,
                column: "nationality_code",
                value: "VNM");

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 191,
                columns: new[] { "nationality_code", "nationality_name" },
                values: new object[] { "GB-WLS", "Welsh" });

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 192,
                columns: new[] { "nationality_code", "nationality_name" },
                values: new object[] { "YEM", "Yemeni" });

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 193,
                columns: new[] { "nationality_code", "nationality_name" },
                values: new object[] { "ZMB", "Zambian" });

            migrationBuilder.InsertData(
                table: "nationalities",
                columns: new[] { "id", "nationality_code", "nationality_name" },
                values: new object[] { 194, "ZWE", "Zimbabwean" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 194);

            migrationBuilder.AlterColumn<string>(
                name: "nationality_code",
                table: "nationalities",
                type: "char(3)",
                fixedLength: true,
                maxLength: 3,
                nullable: false,
                collation: "utf8mb4_0900_ai_ci",
                oldClrType: typeof(string),
                oldType: "char(6)",
                oldFixedLength: true,
                oldMaxLength: 6)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "utf8mb4_0900_ai_ci");

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
                columns: new[] { "nationality_code", "nationality_name" },
                values: new object[] { "", "Saint Lucian" });

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 147,
                columns: new[] { "nationality_code", "nationality_name" },
                values: new object[] { "", "Salvadoran" });

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 148,
                columns: new[] { "nationality_code", "nationality_name" },
                values: new object[] { "", "Samoan" });

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 149,
                columns: new[] { "nationality_code", "nationality_name" },
                values: new object[] { "", "San Marinese" });

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 150,
                columns: new[] { "nationality_code", "nationality_name" },
                values: new object[] { "", "Sao Tomean" });

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 151,
                columns: new[] { "nationality_code", "nationality_name" },
                values: new object[] { "", "Saudi" });

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 152,
                columns: new[] { "nationality_code", "nationality_name" },
                values: new object[] { "", "Scottish" });

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 153,
                columns: new[] { "nationality_code", "nationality_name" },
                values: new object[] { "", "Senegalese" });

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 154,
                columns: new[] { "nationality_code", "nationality_name" },
                values: new object[] { "", "Serbian" });

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 155,
                columns: new[] { "nationality_code", "nationality_name" },
                values: new object[] { "", "Seychellois" });

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 156,
                columns: new[] { "nationality_code", "nationality_name" },
                values: new object[] { "", "Sierra Leonean" });

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 157,
                columns: new[] { "nationality_code", "nationality_name" },
                values: new object[] { "", "Singaporean" });

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 158,
                columns: new[] { "nationality_code", "nationality_name" },
                values: new object[] { "", "Slovak" });

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 159,
                columns: new[] { "nationality_code", "nationality_name" },
                values: new object[] { "", "Slovenian" });

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 160,
                columns: new[] { "nationality_code", "nationality_name" },
                values: new object[] { "", "Solomon Islander" });

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 161,
                columns: new[] { "nationality_code", "nationality_name" },
                values: new object[] { "", "Somali" });

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 162,
                columns: new[] { "nationality_code", "nationality_name" },
                values: new object[] { "", "South African" });

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 163,
                columns: new[] { "nationality_code", "nationality_name" },
                values: new object[] { "", "South Korean" });

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 164,
                columns: new[] { "nationality_code", "nationality_name" },
                values: new object[] { "", "South Sudanese" });

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
                columns: new[] { "nationality_code", "nationality_name" },
                values: new object[] { "", "Surinamer" });

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
                columns: new[] { "nationality_code", "nationality_name" },
                values: new object[] { "", "Turkmen" });

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 183,
                columns: new[] { "nationality_code", "nationality_name" },
                values: new object[] { "", "Tuvaluan" });

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 184,
                columns: new[] { "nationality_code", "nationality_name" },
                values: new object[] { "", "Ugandan" });

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 185,
                columns: new[] { "nationality_code", "nationality_name" },
                values: new object[] { "", "Ukrainian" });

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 186,
                columns: new[] { "nationality_code", "nationality_name" },
                values: new object[] { "", "Uruguayan" });

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 187,
                columns: new[] { "nationality_code", "nationality_name" },
                values: new object[] { "", "Uzbekistani" });

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 188,
                columns: new[] { "nationality_code", "nationality_name" },
                values: new object[] { "", "Vanuatuan" });

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
                columns: new[] { "nationality_code", "nationality_name" },
                values: new object[] { "", "Yemeni" });

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 192,
                columns: new[] { "nationality_code", "nationality_name" },
                values: new object[] { "", "Zambian" });

            migrationBuilder.UpdateData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 193,
                columns: new[] { "nationality_code", "nationality_name" },
                values: new object[] { "", "Zimbabwean" });
        }
    }
}
