using System;

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Stambat.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CardManagementMigration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "LastStampedAt",
                table: "WalletPass",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AddColumn<string>(
                name: "EarnedStampUrl",
                table: "CardTemplate",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmptyStampUrl",
                table: "CardTemplate",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LogoUrlOverride",
                table: "CardTemplate",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TermsAndConditions",
                table: "CardTemplate",
                type: "text",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Description", "IsDeleted", "Name", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new Guid("019cd46a-80b3-7a1a-a1b1-254e15e297db"), new DateTime(2025, 10, 15, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("a0000000-0000-7000-8000-000000000000"), "Middle management for specific tenant features and staff management.", false, "Manager", null, null });

            migrationBuilder.InsertData(
                table: "RolePermissions",
                columns: new[] { "PermissionId", "RoleId" },
                values: new object[,]
                {
                    { new Guid("019cd45e-7cf2-7f36-9595-2bd07dc9e25b"), new Guid("019cd46a-80b3-7a1a-a1b1-254e15e297db") },
                    { new Guid("019cd45e-7cfc-7144-85d7-a624389a5bda"), new Guid("019cd46a-80b3-7a1a-a1b1-254e15e297db") },
                    { new Guid("019cd45e-7cfc-717e-b9df-56b9565cc5e1"), new Guid("019cd46a-80b3-7a1a-a1b1-254e15e297db") },
                    { new Guid("019cd45e-7cfc-71fd-b726-7446578cf8b8"), new Guid("019cd46a-80b3-7a1a-a1b1-254e15e297db") },
                    { new Guid("019cd45e-7cfc-735f-ac03-e6b5ebe67f6e"), new Guid("019cd46a-80b3-7a1a-a1b1-254e15e297db") },
                    { new Guid("019cd45e-7cfc-75ec-9acf-136b67adda34"), new Guid("019cd46a-80b3-7a1a-a1b1-254e15e297db") },
                    { new Guid("019cd45e-7cfc-78ba-b9fc-d69399cdc202"), new Guid("019cd46a-80b3-7a1a-a1b1-254e15e297db") },
                    { new Guid("019cd45e-7cfc-78d6-8548-e20dbdadc04b"), new Guid("019cd46a-80b3-7a1a-a1b1-254e15e297db") },
                    { new Guid("019cd45e-7cfc-7947-88e3-7c06f899920f"), new Guid("019cd46a-80b3-7a1a-a1b1-254e15e297db") },
                    { new Guid("019cd45e-7cfc-7ef1-9e1a-8ae8e3ff78f6"), new Guid("019cd46a-80b3-7a1a-a1b1-254e15e297db") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("019cd45e-7cf2-7f36-9595-2bd07dc9e25b"), new Guid("019cd46a-80b3-7a1a-a1b1-254e15e297db") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("019cd45e-7cfc-7144-85d7-a624389a5bda"), new Guid("019cd46a-80b3-7a1a-a1b1-254e15e297db") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("019cd45e-7cfc-717e-b9df-56b9565cc5e1"), new Guid("019cd46a-80b3-7a1a-a1b1-254e15e297db") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("019cd45e-7cfc-71fd-b726-7446578cf8b8"), new Guid("019cd46a-80b3-7a1a-a1b1-254e15e297db") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("019cd45e-7cfc-735f-ac03-e6b5ebe67f6e"), new Guid("019cd46a-80b3-7a1a-a1b1-254e15e297db") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("019cd45e-7cfc-75ec-9acf-136b67adda34"), new Guid("019cd46a-80b3-7a1a-a1b1-254e15e297db") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("019cd45e-7cfc-78ba-b9fc-d69399cdc202"), new Guid("019cd46a-80b3-7a1a-a1b1-254e15e297db") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("019cd45e-7cfc-78d6-8548-e20dbdadc04b"), new Guid("019cd46a-80b3-7a1a-a1b1-254e15e297db") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("019cd45e-7cfc-7947-88e3-7c06f899920f"), new Guid("019cd46a-80b3-7a1a-a1b1-254e15e297db") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("019cd45e-7cfc-7ef1-9e1a-8ae8e3ff78f6"), new Guid("019cd46a-80b3-7a1a-a1b1-254e15e297db") });

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("019cd46a-80b3-7a1a-a1b1-254e15e297db"));

            migrationBuilder.DropColumn(
                name: "EarnedStampUrl",
                table: "CardTemplate");

            migrationBuilder.DropColumn(
                name: "EmptyStampUrl",
                table: "CardTemplate");

            migrationBuilder.DropColumn(
                name: "LogoUrlOverride",
                table: "CardTemplate");

            migrationBuilder.DropColumn(
                name: "TermsAndConditions",
                table: "CardTemplate");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastStampedAt",
                table: "WalletPass",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);
        }
    }
}
