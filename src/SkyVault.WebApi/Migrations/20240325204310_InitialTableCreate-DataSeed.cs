using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SkyVault.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialTableCreateDataSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "communication_methods",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    comm_title = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "countries",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    country_code = table.Column<string>(type: "varchar(5)", maxLength: 5, nullable: true, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    country_name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "nationalities",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nationality_name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "notification_types",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    type_name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "salutations",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    salutation_name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "system_users",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    first_name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    last_name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    user_role = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    sam_profile_id = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    profile_picture = table.Column<string>(type: "text", nullable: true, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    active = table.Column<string>(type: "char(1)", fixedLength: true, maxLength: 1, nullable: true, defaultValueSql: "'1'", collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "notification_templates",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    content = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    notification_type = table.Column<int>(type: "int", nullable: false),
                    file = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "fk_notification_templates_notification_types",
                        column: x => x.notification_type,
                        principalTable: "notification_types",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "customer_profiles",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    salutation_id = table.Column<int>(type: "int", nullable: false),
                    preferred_comm_id = table.Column<int>(type: "int", nullable: false),
                    system_user_id = table.Column<int>(type: "int", nullable: false),
                    parent_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "fk_customerprofiles_communication_methods",
                        column: x => x.preferred_comm_id,
                        principalTable: "communication_methods",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_customerprofiles_customerprofiles",
                        column: x => x.parent_id,
                        principalTable: "customer_profiles",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_customerprofiles_salutations",
                        column: x => x.salutation_id,
                        principalTable: "salutations",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_customerprofiles_system_users",
                        column: x => x.system_user_id,
                        principalTable: "system_users",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "frequent_flyer_numbers",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    flyer_number = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    customer_profile_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "fk_frequentflyernumbers_customerprofiles",
                        column: x => x.customer_profile_id,
                        principalTable: "customer_profiles",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "jobs",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    date_time = table.Column<DateOnly>(type: "date", nullable: false),
                    status = table.Column<string>(type: "char(1)", fixedLength: true, maxLength: 1, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    customer_profile_id = table.Column<int>(type: "int", nullable: false),
                    template_id = table.Column<int>(type: "int", nullable: false),
                    log = table.Column<string>(type: "text", nullable: true, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "fk_jobs_customerprofiles",
                        column: x => x.customer_profile_id,
                        principalTable: "customer_profiles",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_jobs_notification_templates",
                        column: x => x.template_id,
                        principalTable: "notification_templates",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "passports",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    customer_profile_id = table.Column<int>(type: "int", nullable: false),
                    last_name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    other_names = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    passport_number = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    gender = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    date_of_birth = table.Column<DateOnly>(type: "date", nullable: false),
                    place_of_birth = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    nationality_id = table.Column<int>(type: "int", nullable: false),
                    is_primary = table.Column<string>(type: "char(1)", fixedLength: true, maxLength: 1, nullable: false, defaultValueSql: "'0'", collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "fk_passports_customerprofiles",
                        column: x => x.customer_profile_id,
                        principalTable: "customer_profiles",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_passports_nationalities",
                        column: x => x.nationality_id,
                        principalTable: "nationalities",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "visas",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    visa_number = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    issued_place = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    issued_date = table.Column<DateOnly>(type: "date", nullable: false),
                    expire_date = table.Column<DateOnly>(type: "date", nullable: false),
                    country_id = table.Column<int>(type: "int", nullable: false),
                    passport_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "fk_visas_countries",
                        column: x => x.country_id,
                        principalTable: "countries",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_visas_passports",
                        column: x => x.passport_id,
                        principalTable: "passports",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.InsertData(
                table: "countries",
                columns: new[] { "id", "country_code", "country_name" },
                values: new object[,]
                {
                    { 1, "AFG", "Afghanistan" },
                    { 2, "ALA", "Albania" },
                    { 3, "ALB", "Algeria" },
                    { 4, "AND", "Andorra" },
                    { 5, "AGO", "Angola" },
                    { 6, "AIA", "Anguilla" },
                    { 7, "ATA", "Antarctica" },
                    { 8, "ATG", "Antigua and Barbuda" },
                    { 9, "ARG", "Argentina" },
                    { 10, "ARM", "Armenia" },
                    { 11, "ABW", "Aruba" },
                    { 12, "AUS", "Australia" },
                    { 13, "AUT", "Austria" },
                    { 14, "AZE", "Azerbaijan" },
                    { 15, "BHS", "Bahamas" },
                    { 16, "BHR", "Bahrain" },
                    { 17, "BGD", "Bangladesh" },
                    { 18, "BRB", "Barbados" },
                    { 19, "BLR", "Belarus" },
                    { 20, "BEL", "Belgium" },
                    { 21, "BLZ", "Belize" },
                    { 22, "BEN", "Benin" },
                    { 23, "BMU", "Bermuda" },
                    { 24, "BTN", "Bhutan" },
                    { 25, "BOL", "Bolivia" },
                    { 26, "BES", "Bonaire, Sint Eustatius and Saba" },
                    { 27, "BIH", "Bosnia and Herzegovina" },
                    { 28, "BWA", "Botswana" },
                    { 29, "BVT", "Bouvet Island" },
                    { 30, "BRA", "Brazil" },
                    { 31, "IOT", "British Indian Ocean Territory" },
                    { 32, "BRN", "Brunei Darussalam" },
                    { 33, "BGR", "Bulgaria" },
                    { 34, "BFA", "Burkina Faso" },
                    { 35, "BDI", "Burundi" },
                    { 36, "CPV", "Cabo Verde" },
                    { 37, "KHM", "Cambodia" },
                    { 38, "CMR", "Cameroon" },
                    { 39, "CAN", "Canada" },
                    { 40, "CYM", "Cayman Islands" },
                    { 41, "CAF", "Central African Republic" },
                    { 42, "TCD", "Chad" },
                    { 43, "CHL", "Chile" },
                    { 44, "CHN", "China" },
                    { 45, "CXR", "Christmas Island" },
                    { 46, "CCK", "Cocos (Keeling) Islands" },
                    { 47, "COL", "Colombia" },
                    { 48, "COM", "Comoros" },
                    { 49, "COG", "Congo" },
                    { 50, "COD", "Congo, Democratic Republic of the" },
                    { 51, "COK", "Cook Islands" },
                    { 52, "CRI", "Costa Rica" },
                    { 53, "HRV", "Croatia" },
                    { 54, "CUB", "Cuba" },
                    { 55, "CUW", "Curaçao" },
                    { 56, "CYP", "Cyprus" },
                    { 57, "CZE", "Czech Republic" },
                    { 58, "DNK", "Denmark" },
                    { 59, "DJI", "Djibouti" },
                    { 60, "DMA", "Dominica" },
                    { 61, "DOM", "Dominican Republic" },
                    { 62, "ECU", "Ecuador" },
                    { 63, "EGY", "Egypt" },
                    { 64, "SLV", "El Salvador" },
                    { 65, "GNQ", "Equatorial Guinea" },
                    { 66, "ERI", "Eritrea" },
                    { 67, "EST", "Estonia" },
                    { 68, "SWZ", "Eswatini" },
                    { 69, "ETH", "Ethiopia" },
                    { 70, "FLK", "Falkland Islands (Malvinas)" },
                    { 71, "FRO", "Faroe Islands" },
                    { 72, "FJI", "Fiji" },
                    { 73, "FIN", "Finland" },
                    { 74, "FRA", "France" },
                    { 75, "GUF", "French Guiana" },
                    { 76, "PYF", "French Polynesia" },
                    { 77, "ATF", "French Southern Territories" },
                    { 78, "GAB", "Gabon" },
                    { 79, "GMB", "Gambia" },
                    { 80, "GEO", "Georgia" },
                    { 81, "DEU", "Germany" },
                    { 82, "GHA", "Ghana" },
                    { 83, "GIB", "Gibraltar" },
                    { 84, "GRC", "Greece" },
                    { 85, "GRL", "Greenland" },
                    { 86, "GRD", "Grenada" },
                    { 87, "GLP", "Guadeloupe" },
                    { 88, "GUM", "Guam" },
                    { 89, "GTM", "Guatemala" },
                    { 90, "GGY", "Guernsey" },
                    { 91, "GIN", "Guinea" },
                    { 92, "GNB", "Guinea-Bissau" },
                    { 93, "GUY", "Guyana" },
                    { 94, "HTI", "Haiti" },
                    { 95, "HMD", "Heard Island and McDonald Islands" },
                    { 96, "VAT", "Holy See" },
                    { 97, "HND", "Honduras" },
                    { 98, "HKG", "Hong Kong" },
                    { 99, "HUN", "Hungary" },
                    { 100, "ISL", "Iceland" },
                    { 101, "IND", "India" },
                    { 102, "IDN", "Indonesia" },
                    { 103, "IRN", "Iran" },
                    { 104, "IRQ", "Iraq" },
                    { 105, "IRL", "Ireland" },
                    { 106, "ISR", "Israel" },
                    { 107, "ITA", "Italy" },
                    { 108, "JAM", "Jamaica" },
                    { 109, "JPN", "Japan" },
                    { 110, "JOR", "Jordan" },
                    { 111, "KAZ", "Kazakhstan" },
                    { 112, "KEN", "Kenya" },
                    { 113, "KIR", "Kiribati" },
                    { 114, "KWT", "Kuwait" },
                    { 115, "KGZ", "Kyrgyzstan" },
                    { 116, "LAO", "Laos" },
                    { 117, "LVA", "Latvia" },
                    { 118, "LBN", "Lebanon" },
                    { 119, "LSO", "Lesotho" },
                    { 120, "LBR", "Liberia" },
                    { 121, "LBY", "Libya" },
                    { 122, "LIE", "Liechtenstein" },
                    { 123, "LTU", "Lithuania" },
                    { 124, "LUX", "Luxembourg" },
                    { 125, "MDG", "Madagascar" },
                    { 126, "MWI", "Malawi" },
                    { 127, "MYS", "Malaysia" },
                    { 128, "MDV", "Maldives" },
                    { 129, "MLI", "Mali" },
                    { 130, "MLT", "Malta" },
                    { 131, "MHL", "Marshall Islands" },
                    { 132, "MRT", "Mauritania" },
                    { 133, "MUS", "Mauritius" },
                    { 134, "MEX", "Mexico" },
                    { 135, "FSM", "Micronesia" },
                    { 136, "MDA", "Moldova" },
                    { 137, "MCO", "Monaco" },
                    { 138, "MNG", "Mongolia" },
                    { 139, "MNE", "Montenegro" },
                    { 140, "MAR", "Morocco" },
                    { 141, "MOZ", "Mozambique" },
                    { 142, "MMR", "Myanmar" },
                    { 143, "NAM", "Namibia" },
                    { 144, "NRU", "Nauru" },
                    { 145, "NPL", "Nepal" },
                    { 146, "NLD", "Netherlands" },
                    { 147, "NZL", "New Zealand" },
                    { 148, "NIC", "Nicaragua" },
                    { 149, "NER", "Niger" },
                    { 150, "NGA", "Nigeria" },
                    { 151, "PRK", "North Korea" },
                    { 152, "MKD", "North Macedonia" },
                    { 153, "NOR", "Norway" },
                    { 154, "OMN", "Oman" },
                    { 155, "PAK", "Pakistan" },
                    { 156, "PLW", "Palau" },
                    { 157, "PSE", "Palestine State" },
                    { 158, "PAN", "Panama" },
                    { 159, "PNG", "Papua New Guinea" },
                    { 160, "PRY", "Paraguay" },
                    { 161, "PER", "Peru" },
                    { 162, "PHL", "Philippines" },
                    { 163, "POL", "Poland" },
                    { 164, "PRT", "Portugal" },
                    { 165, "QAT", "Qatar" },
                    { 166, "ROU", "Romania" },
                    { 167, "RUS", "Russia" },
                    { 168, "RWA", "Rwanda" },
                    { 169, "KNA", "Saint Kitts and Nevis" },
                    { 170, "LCA", "Saint Lucia" },
                    { 171, "VCT", "Saint Vincent and the Grenadines" },
                    { 172, "WSM", "Samoa" },
                    { 173, "SMR", "San Marino" },
                    { 174, "STP", "Sao Tome and Principe" },
                    { 175, "SAU", "Saudi Arabia" },
                    { 176, "SEN", "Senegal" },
                    { 177, "SRB", "Serbia" },
                    { 178, "SYC", "Seychelles" },
                    { 179, "SLE", "Sierra Leone" },
                    { 180, "SGP", "Singapore" },
                    { 181, "SVK", "Slovakia" },
                    { 182, "SVN", "Slovenia" },
                    { 183, "SLB", "Solomon Islands" },
                    { 184, "SOM", "Somalia" },
                    { 185, "ZAF", "South Africa" },
                    { 186, "KOR", "South Korea" },
                    { 187, "SSD", "South Sudan" },
                    { 188, "ESP", "Spain" },
                    { 189, "LKA", "Sri Lanka" },
                    { 190, "SDN", "Sudan" },
                    { 191, "SUR", "Suriname" },
                    { 192, "SWE", "Sweden" },
                    { 193, "CHE", "Switzerland" },
                    { 194, "SYR", "Syria" },
                    { 195, "TJK", "Tajikistan" },
                    { 196, "TZA", "Tanzania" },
                    { 197, "THA", "Thailand" },
                    { 198, "TLS", "Timor-Leste" },
                    { 199, "TGO", "Togo" },
                    { 200, "TON", "Tonga" },
                    { 201, "TTO", "Trinidad and Tobago" },
                    { 202, "TUN", "Tunisia" },
                    { 203, "TUR", "Turkey" },
                    { 204, "TKM", "Turkmenistan" },
                    { 205, "TUV", "Tuvalu" },
                    { 206, "UGA", "Uganda" },
                    { 207, "UKR", "Ukraine" },
                    { 208, "ARE", "United Arab Emirates" },
                    { 209, "GBR", "United Kingdom" },
                    { 210, "USA", "United States of America" },
                    { 211, "URY", "Uruguay" },
                    { 212, "UZB", "Uzbekistan" },
                    { 213, "VUT", "Vanuatu" },
                    { 214, "VEN", "Venezuela" },
                    { 215, "VNM", "Vietnam" },
                    { 216, "YEM", "Yemen" },
                    { 217, "ZMB", "Zambia" },
                    { 218, "ZWE", "Zimbabwe" }
                });

            migrationBuilder.InsertData(
                table: "nationalities",
                columns: new[] { "id", "nationality_name" },
                values: new object[,]
                {
                    { 1, "Afghan" },
                    { 2, "Albanian" },
                    { 3, "Algerian" },
                    { 4, "American" },
                    { 5, "Andorran" },
                    { 6, "Angolan" },
                    { 7, "Antiguans" },
                    { 8, "Argentinean" },
                    { 9, "Armenian" },
                    { 10, "Australian" },
                    { 11, "Austrian" },
                    { 12, "Azerbaijani" },
                    { 13, "Bahamian" },
                    { 14, "Bahraini" },
                    { 15, "Bangladeshi" },
                    { 16, "Barbadian" },
                    { 17, "Belarusian" },
                    { 18, "Belgian" },
                    { 19, "Belizean" },
                    { 20, "Beninese" },
                    { 21, "Bhutanese" },
                    { 22, "Bolivian" },
                    { 23, "Bosnian" },
                    { 24, "Botswanan" },
                    { 25, "Brazilian" },
                    { 26, "British" },
                    { 27, "Bruneian" },
                    { 28, "Bulgarian" },
                    { 29, "Burkinabe" },
                    { 30, "Burundian" },
                    { 31, "Cambodian" },
                    { 32, "Cameroonian" },
                    { 33, "Canadian" },
                    { 34, "Cape Verdean" },
                    { 35, "Central African" },
                    { 36, "Chadian" },
                    { 37, "Chilean" },
                    { 38, "Chinese" },
                    { 39, "Colombian" },
                    { 40, "Comoran" },
                    { 41, "Congolese" },
                    { 42, "Costa Rican" },
                    { 43, "Croatian" },
                    { 44, "Cuban" },
                    { 45, "Cypriot" },
                    { 46, "Czech" },
                    { 47, "Danish" },
                    { 48, "Djiboutian" },
                    { 49, "Dominican" },
                    { 50, "Dutch" },
                    { 51, "East Timorese" },
                    { 52, "Ecuadorean" },
                    { 53, "Egyptian" },
                    { 54, "Emirian" },
                    { 55, "Equatorial Guinean" },
                    { 56, "Eritrean" },
                    { 57, "Estonian" },
                    { 58, "Ethiopian" },
                    { 59, "Fijian" },
                    { 60, "Filipino" },
                    { 61, "Finnish" },
                    { 62, "French" },
                    { 63, "Gabonese" },
                    { 64, "Gambian" },
                    { 65, "Georgian" },
                    { 66, "German" },
                    { 67, "Ghanaian" },
                    { 68, "Greek" },
                    { 69, "Grenadian" },
                    { 70, "Guatemalan" },
                    { 71, "Guinea-Bissauan" },
                    { 72, "Guinean" },
                    { 73, "Guyanese" },
                    { 74, "Haitian" },
                    { 75, "Herzegovinian" },
                    { 76, "Honduran" },
                    { 77, "Hungarian" },
                    { 78, "I-Kiribati" },
                    { 79, "Icelander" },
                    { 80, "Indian" },
                    { 81, "Indonesian" },
                    { 82, "Iranian" },
                    { 83, "Iraqi" },
                    { 84, "Irish" },
                    { 85, "Israeli" },
                    { 86, "Italian" },
                    { 87, "Ivorian" },
                    { 88, "Jamaican" },
                    { 89, "Japanese" },
                    { 90, "Jordanian" },
                    { 91, "Kazakhstani" },
                    { 92, "Kenyan" },
                    { 93, "Kittian and Nevisian" },
                    { 94, "Kuwaiti" },
                    { 95, "Kyrgyz" },
                    { 96, "Laotian" },
                    { 97, "Latvian" },
                    { 98, "Lebanese" },
                    { 99, "Liberian" },
                    { 100, "Libyan" },
                    { 101, "Liechtensteiner" },
                    { 102, "Lithuanian" },
                    { 103, "Luxembourger" },
                    { 104, "Malagasy" },
                    { 105, "Malawian" },
                    { 106, "Malaysian" },
                    { 107, "Maldivan" },
                    { 108, "Malian" },
                    { 109, "Maltese" },
                    { 110, "Marshallese" },
                    { 111, "Mauritanian" },
                    { 112, "Mauritian" },
                    { 113, "Mexican" },
                    { 114, "Micronesian" },
                    { 115, "Moldovan" },
                    { 116, "Monacan" },
                    { 117, "Mongolian" },
                    { 118, "Montenegrin" },
                    { 119, "Moroccan" },
                    { 120, "Mosotho" },
                    { 121, "Motswana" },
                    { 122, "Mozambican" },
                    { 123, "Namibian" },
                    { 124, "Nauruan" },
                    { 125, "Nepalese" },
                    { 126, "Netherlander" },
                    { 127, "New Zealander" },
                    { 128, "Nicaraguan" },
                    { 129, "Nigerian" },
                    { 130, "North Korean" },
                    { 131, "Northern Irish" },
                    { 132, "Norwegian" },
                    { 133, "Omani" },
                    { 134, "Pakistani" },
                    { 135, "Palauan" },
                    { 136, "Panamanian" },
                    { 137, "Papua New Guinean" },
                    { 138, "Paraguayan" },
                    { 139, "Peruvian" },
                    { 140, "Polish" },
                    { 141, "Portuguese" },
                    { 142, "Qatari" },
                    { 143, "Romanian" },
                    { 144, "Russian" },
                    { 145, "Rwandan" },
                    { 146, "Saint Lucian" },
                    { 147, "Salvadoran" },
                    { 148, "Samoan" },
                    { 149, "San Marinese" },
                    { 150, "Sao Tomean" },
                    { 151, "Saudi" },
                    { 152, "Scottish" },
                    { 153, "Senegalese" },
                    { 154, "Serbian" },
                    { 155, "Seychellois" },
                    { 156, "Sierra Leonean" },
                    { 157, "Singaporean" },
                    { 158, "Slovak" },
                    { 159, "Slovenian" },
                    { 160, "Solomon Islander" },
                    { 161, "Somali" },
                    { 162, "South African" },
                    { 163, "South Korean" },
                    { 164, "South Sudanese" },
                    { 165, "Spanish" },
                    { 166, "Sri Lankan" },
                    { 167, "Sudanese" },
                    { 168, "Surinamer" },
                    { 169, "Swazi" },
                    { 170, "Swedish" },
                    { 171, "Swiss" },
                    { 172, "Syrian" },
                    { 173, "Taiwanese" },
                    { 174, "Tajik" },
                    { 175, "Tanzanian" },
                    { 176, "Thai" },
                    { 177, "Togolese" },
                    { 178, "Tongan" },
                    { 179, "Trinidadian or Tobagonian" },
                    { 180, "Tunisian" },
                    { 181, "Turkish" },
                    { 182, "Turkmen" },
                    { 183, "Tuvaluan" },
                    { 184, "Ugandan" },
                    { 185, "Ukrainian" },
                    { 186, "Uruguayan" },
                    { 187, "Uzbekistani" },
                    { 188, "Vanuatuan" },
                    { 189, "Venezuelan" },
                    { 190, "Vietnamese" },
                    { 191, "Yemeni" },
                    { 192, "Zambian" },
                    { 193, "Zimbabwean" }
                });

            migrationBuilder.InsertData(
                table: "salutations",
                columns: new[] { "id", "salutation_name" },
                values: new object[,]
                {
                    { 1, "Mr" },
                    { 2, "Mrs" },
                    { 3, "Miss" },
                    { 4, "Dr" },
                    { 5, "Prof" },
                    { 6, "Rev" },
                    { 7, "Hon" }
                });

            migrationBuilder.CreateIndex(
                name: "fk_customerprofiles_communication_methods",
                table: "customer_profiles",
                column: "preferred_comm_id");

            migrationBuilder.CreateIndex(
                name: "fk_customerprofiles_customerprofiles",
                table: "customer_profiles",
                column: "parent_id");

            migrationBuilder.CreateIndex(
                name: "fk_customerprofiles_salutations",
                table: "customer_profiles",
                column: "salutation_id");

            migrationBuilder.CreateIndex(
                name: "fk_customerprofiles_system_users",
                table: "customer_profiles",
                column: "system_user_id");

            migrationBuilder.CreateIndex(
                name: "fk_frequentflyernumbers_customerprofiles",
                table: "frequent_flyer_numbers",
                column: "customer_profile_id");

            migrationBuilder.CreateIndex(
                name: "fk_jobs_customerprofiles",
                table: "jobs",
                column: "customer_profile_id");

            migrationBuilder.CreateIndex(
                name: "fk_jobs_notification_templates",
                table: "jobs",
                column: "template_id");

            migrationBuilder.CreateIndex(
                name: "fk_notification_templates_notification_types",
                table: "notification_templates",
                column: "notification_type");

            migrationBuilder.CreateIndex(
                name: "fk_passports_customerprofiles",
                table: "passports",
                column: "customer_profile_id");

            migrationBuilder.CreateIndex(
                name: "fk_passports_nationalities",
                table: "passports",
                column: "nationality_id");

            migrationBuilder.CreateIndex(
                name: "fk_visas_countries",
                table: "visas",
                column: "country_id");

            migrationBuilder.CreateIndex(
                name: "fk_visas_passports",
                table: "visas",
                column: "passport_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "frequent_flyer_numbers");

            migrationBuilder.DropTable(
                name: "jobs");

            migrationBuilder.DropTable(
                name: "visas");

            migrationBuilder.DropTable(
                name: "notification_templates");

            migrationBuilder.DropTable(
                name: "countries");

            migrationBuilder.DropTable(
                name: "passports");

            migrationBuilder.DropTable(
                name: "notification_types");

            migrationBuilder.DropTable(
                name: "customer_profiles");

            migrationBuilder.DropTable(
                name: "nationalities");

            migrationBuilder.DropTable(
                name: "communication_methods");

            migrationBuilder.DropTable(
                name: "salutations");

            migrationBuilder.DropTable(
                name: "system_users");
        }
    }
}
