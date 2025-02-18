using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SkyVault.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class AddCreatedByAndUpdatedByToNotificationTemplate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "notification_templates",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "notification_templates",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "notification_templates",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "notification_templates",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedBy",
                table: "notification_templates",
                type: "int",
                nullable: true);

            migrationBuilder.InsertData(
                table: "notification_types",
                columns: new[] { "id", "type_name" },
                values: new object[,]
                {
                    { 1, "Birthday" },
                    { 2, "PassportExpiry" },
                    { 3, "VisaExpiry" },
                    { 4, "Custom" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_notification_templates_CreatedBy",
                table: "notification_templates",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_notification_templates_UpdatedBy",
                table: "notification_templates",
                column: "UpdatedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_notification_templates_system_users_CreatedBy",
                table: "notification_templates",
                column: "CreatedBy",
                principalTable: "system_users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_notification_templates_system_users_UpdatedBy",
                table: "notification_templates",
                column: "UpdatedBy",
                principalTable: "system_users",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_notification_templates_system_users_CreatedBy",
                table: "notification_templates");

            migrationBuilder.DropForeignKey(
                name: "FK_notification_templates_system_users_UpdatedBy",
                table: "notification_templates");

            migrationBuilder.DropIndex(
                name: "IX_notification_templates_CreatedBy",
                table: "notification_templates");

            migrationBuilder.DropIndex(
                name: "IX_notification_templates_UpdatedBy",
                table: "notification_templates");

            migrationBuilder.DeleteData(
                table: "notification_types",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "notification_types",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "notification_types",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "notification_types",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.DropColumn(
                name: "Active",
                table: "notification_templates");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "notification_templates");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "notification_templates");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "notification_templates");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "notification_templates");
        }
    }
}
