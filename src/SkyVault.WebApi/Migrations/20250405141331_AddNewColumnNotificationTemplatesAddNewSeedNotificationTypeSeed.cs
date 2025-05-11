using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SkyVault.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class AddNewColumnNotificationTemplatesAddNewSeedNotificationTypeSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CommunicationMethodId",
                table: "notification_templates",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "notification_types",
                columns: new[] { "id", "type_name" },
                values: new object[] { 5, "Emergency" });

            migrationBuilder.CreateIndex(
                name: "IX_notification_templates_CommunicationMethodId",
                table: "notification_templates",
                column: "CommunicationMethodId");

            migrationBuilder.AddForeignKey(
                name: "FK_notification_templates_communication_methods_CommunicationMe~",
                table: "notification_templates",
                column: "CommunicationMethodId",
                principalTable: "communication_methods",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_notification_templates_communication_methods_CommunicationMe~",
                table: "notification_templates");

            migrationBuilder.DropIndex(
                name: "IX_notification_templates_CommunicationMethodId",
                table: "notification_templates");

            migrationBuilder.DeleteData(
                table: "notification_types",
                keyColumn: "id",
                keyValue: 5);

            migrationBuilder.DropColumn(
                name: "CommunicationMethodId",
                table: "notification_templates");
        }
    }
}
