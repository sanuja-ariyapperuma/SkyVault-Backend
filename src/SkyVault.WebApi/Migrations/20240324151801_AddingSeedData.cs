using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SkyVault.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class AddingSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "countries",
                columns: new[] { "id", "country_code", "country_name" },
                values: new object[,]
                {
                    { 1, null, "Afghanistan" },
                    { 2, null, "Albania" },
                    { 3, null, "Algeria" },
                    { 4, null, "Andorra" },
                    { 5, null, "Angola" },
                    { 6, null, "Antigua and Barbuda" },
                    { 7, null, "Argentina" },
                    { 8, null, "Armenia" },
                    { 9, null, "Australia" },
                    { 10, null, "Austria" },
                    { 11, null, "Azerbaijan" },
                    { 12, null, "Bahamas" },
                    { 13, null, "Bahrain" },
                    { 14, null, "Bangladesh" },
                    { 15, null, "Barbados" },
                    { 16, null, "Belarus" },
                    { 17, null, "Belgium" },
                    { 18, null, "Belize" },
                    { 19, null, "Benin" },
                    { 20, null, "Bhutan" },
                    { 21, null, "Bolivia" },
                    { 22, null, "Bosnia and Herzegovina" },
                    { 23, null, "Botswana" },
                    { 24, null, "Brazil" },
                    { 25, null, "Brunei" },
                    { 26, null, "Bulgaria" },
                    { 27, null, "Burkina Faso" },
                    { 28, null, "Burundi" },
                    { 29, null, "Cabo Verde" },
                    { 30, null, "Cambodia" },
                    { 31, null, "Cameroon" },
                    { 32, null, "Canada" },
                    { 33, null, "Central African Republic" },
                    { 34, null, "Chad" },
                    { 35, null, "Chile" },
                    { 36, null, "China" },
                    { 37, null, "Colombia" },
                    { 38, null, "Comoros" },
                    { 39, null, "Congo (Congo-Brazzaville)" },
                    { 40, null, "Costa Rica" },
                    { 41, null, "Croatia" },
                    { 42, null, "Cuba" },
                    { 43, null, "Cyprus" },
                    { 44, null, "Czechia (Czech Republic)" },
                    { 45, null, "Democratic Republic of the Congo" },
                    { 46, null, "Denmark" },
                    { 47, null, "Djibouti" },
                    { 48, null, "Dominica" },
                    { 49, null, "Dominican Republic" },
                    { 50, null, "Ecuador" },
                    { 51, null, "Egypt" },
                    { 52, null, "El Salvador" },
                    { 53, null, "Equatorial Guinea" },
                    { 54, null, "Eritrea" },
                    { 55, null, "Estonia" },
                    { 56, null, "Swaziland" },
                    { 57, null, "Ethiopia" },
                    { 58, null, "Fiji" },
                    { 59, null, "Finland" },
                    { 60, null, "France" },
                    { 61, null, "Gabon" },
                    { 62, null, "Gambia" },
                    { 63, null, "Georgia" },
                    { 64, null, "Germany" },
                    { 65, null, "Ghana" },
                    { 66, null, "Greece" },
                    { 67, null, "Grenada" },
                    { 68, null, "Guatemala" },
                    { 69, null, "Guinea" },
                    { 70, null, "Guinea-Bissau" },
                    { 71, null, "Guyana" },
                    { 72, null, "Haiti" },
                    { 73, null, "Holy See" },
                    { 74, null, "Honduras" },
                    { 75, null, "Hungary" },
                    { 76, null, "Iceland" },
                    { 77, null, "India" },
                    { 78, null, "Indonesia" },
                    { 79, null, "Iran" },
                    { 80, null, "Iraq" },
                    { 81, null, "Ireland" },
                    { 82, null, "Israel" },
                    { 83, null, "Italy" },
                    { 84, null, "Jamaica" },
                    { 85, null, "Japan" },
                    { 86, null, "Jordan" },
                    { 87, null, "Kazakhstan" },
                    { 88, null, "Kenya" },
                    { 89, null, "Kiribati" },
                    { 90, null, "Kuwait" },
                    { 91, null, "Kyrgyzstan" },
                    { 92, null, "Laos" },
                    { 93, null, "Latvia" },
                    { 94, null, "Lebanon" },
                    { 95, null, "Lesotho" },
                    { 96, null, "Liberia" },
                    { 97, null, "Libya" },
                    { 98, null, "Liechtenstein" },
                    { 99, null, "Lithuania" },
                    { 100, null, "Luxembourg" },
                    { 101, null, "Madagascar" },
                    { 102, null, "Malawi" },
                    { 103, null, "Malaysia" },
                    { 104, null, "Maldives" },
                    { 105, null, "Mali" },
                    { 106, null, "Malta" },
                    { 107, null, "Marshall Islands" },
                    { 108, null, "Mauritania" },
                    { 109, null, "Mauritius" },
                    { 110, null, "Mexico" },
                    { 111, null, "Micronesia" },
                    { 112, null, "Moldova" },
                    { 113, null, "Monaco" },
                    { 114, null, "Mongolia" },
                    { 115, null, "Montenegro" },
                    { 116, null, "Morocco" },
                    { 117, null, "Mozambique" },
                    { 118, null, "Myanmar" },
                    { 119, null, "Namibia" },
                    { 120, null, "Nauru" },
                    { 121, null, "Nepal" },
                    { 122, null, "Netherlands" },
                    { 123, null, "New Zealand" },
                    { 124, null, "Nicaragua" },
                    { 125, null, "Niger" },
                    { 126, null, "Nigeria" },
                    { 127, null, "North Korea" },
                    { 128, null, "North Macedonia" },
                    { 129, null, "Norway" },
                    { 130, null, "Oman" },
                    { 131, null, "Pakistan" },
                    { 132, null, "Palau" },
                    { 133, null, "Palestine State" },
                    { 134, null, "Panama" },
                    { 135, null, "Papua New Guinea" },
                    { 136, null, "Paraguay" },
                    { 137, null, "Peru" },
                    { 138, null, "Philippines" },
                    { 139, null, "Poland" },
                    { 140, null, "Portugal" },
                    { 141, null, "Qatar" },
                    { 142, null, "Romania" },
                    { 143, null, "Russia" },
                    { 144, null, "Rwanda" },
                    { 145, null, "Saint Kitts and Nevis" },
                    { 146, null, "Saint Lucia" },
                    { 147, null, "Saint Vincent and the Grenadines" },
                    { 148, null, "Samoa" },
                    { 149, null, "San Marino" },
                    { 150, null, "Sao Tome and Principe" },
                    { 151, null, "Saudi Arabia" },
                    { 152, null, "Senegal" },
                    { 153, null, "Serbia" },
                    { 154, null, "Seychelles" },
                    { 155, null, "Sierra Leone" },
                    { 156, null, "Singapore" },
                    { 157, null, "Slovakia" },
                    { 158, null, "Slovenia" },
                    { 159, null, "Solomon Islands" },
                    { 160, null, "Somalia" },
                    { 161, null, "South Africa" },
                    { 162, null, "South Korea" },
                    { 163, null, "South Sudan" },
                    { 164, null, "Spain" },
                    { 165, null, "Sri Lanka" },
                    { 166, null, "Sudan" },
                    { 167, null, "Suriname" },
                    { 168, null, "Sweden" },
                    { 169, null, "Switzerland" },
                    { 170, null, "Syria" },
                    { 171, null, "Tajikistan" },
                    { 172, null, "Tanzania" },
                    { 173, null, "Thailand" },
                    { 174, null, "Timor-Leste" },
                    { 175, null, "Togo" },
                    { 176, null, "Tonga" },
                    { 177, null, "Trinidad and Tobago" },
                    { 178, null, "Tunisia" },
                    { 179, null, "Turkey" },
                    { 180, null, "Turkmenistan" },
                    { 181, null, "Tuvalu" },
                    { 182, null, "Uganda" },
                    { 183, null, "Ukraine" },
                    { 184, null, "United Arab Emirates" },
                    { 185, null, "United Kingdom" },
                    { 186, null, "United States of America" },
                    { 187, null, "Uruguay" },
                    { 188, null, "Uzbekistan" },
                    { 189, null, "Vanuatu" },
                    { 190, null, "Venezuela" },
                    { 191, null, "Vietnam" },
                    { 192, null, "Yemen" },
                    { 193, null, "Zambia" },
                    { 194, null, "Zimbabwe" }
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 57);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 58);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 59);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 60);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 61);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 62);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 63);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 64);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 65);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 66);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 67);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 68);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 69);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 70);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 71);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 72);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 73);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 74);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 75);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 76);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 77);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 78);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 79);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 80);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 81);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 82);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 83);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 84);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 85);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 86);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 87);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 88);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 89);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 90);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 91);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 92);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 93);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 94);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 95);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 96);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 97);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 98);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 99);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 100);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 101);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 102);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 103);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 104);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 105);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 106);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 107);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 108);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 109);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 110);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 111);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 112);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 113);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 114);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 115);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 116);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 117);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 118);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 119);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 120);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 121);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 122);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 123);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 124);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 125);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 126);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 127);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 128);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 129);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 130);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 131);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 132);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 133);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 134);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 135);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 136);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 137);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 138);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 139);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 140);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 141);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 142);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 143);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 144);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 145);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 146);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 147);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 148);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 149);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 150);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 151);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 152);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 153);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 154);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 155);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 156);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 157);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 158);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 159);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 160);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 161);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 162);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 163);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 164);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 165);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 166);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 167);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 168);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 169);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 170);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 171);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 172);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 173);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 174);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 175);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 176);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 177);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 178);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 179);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 180);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 181);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 182);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 183);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 184);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 185);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 186);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 187);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 188);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 189);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 190);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 191);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 192);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 193);

            migrationBuilder.DeleteData(
                table: "countries",
                keyColumn: "id",
                keyValue: 194);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 57);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 58);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 59);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 60);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 61);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 62);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 63);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 64);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 65);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 66);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 67);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 68);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 69);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 70);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 71);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 72);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 73);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 74);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 75);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 76);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 77);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 78);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 79);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 80);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 81);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 82);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 83);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 84);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 85);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 86);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 87);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 88);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 89);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 90);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 91);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 92);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 93);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 94);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 95);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 96);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 97);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 98);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 99);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 100);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 101);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 102);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 103);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 104);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 105);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 106);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 107);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 108);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 109);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 110);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 111);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 112);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 113);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 114);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 115);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 116);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 117);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 118);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 119);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 120);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 121);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 122);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 123);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 124);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 125);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 126);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 127);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 128);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 129);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 130);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 131);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 132);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 133);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 134);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 135);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 136);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 137);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 138);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 139);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 140);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 141);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 142);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 143);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 144);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 145);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 146);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 147);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 148);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 149);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 150);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 151);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 152);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 153);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 154);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 155);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 156);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 157);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 158);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 159);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 160);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 161);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 162);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 163);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 164);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 165);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 166);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 167);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 168);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 169);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 170);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 171);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 172);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 173);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 174);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 175);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 176);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 177);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 178);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 179);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 180);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 181);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 182);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 183);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 184);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 185);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 186);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 187);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 188);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 189);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 190);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 191);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 192);

            migrationBuilder.DeleteData(
                table: "nationalities",
                keyColumn: "id",
                keyValue: 193);

            migrationBuilder.DeleteData(
                table: "salutations",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "salutations",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "salutations",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "salutations",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "salutations",
                keyColumn: "id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "salutations",
                keyColumn: "id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "salutations",
                keyColumn: "id",
                keyValue: 7);
        }
    }
}
