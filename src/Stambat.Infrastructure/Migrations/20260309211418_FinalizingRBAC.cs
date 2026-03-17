using System;

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Stambat.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FinalizingRBAC : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Invitations_Token",
                table: "Invitations");

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("55555555-5555-7555-8555-555555555555"), new Guid("22222222-2222-7222-8222-222222222222") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("55555555-5555-7555-8555-555555555555"), new Guid("33333333-3333-7333-8333-333333333333") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("55555555-5555-7555-8555-555555555555"), new Guid("44444444-4444-7444-8444-444444444444") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("66666666-6666-7666-8666-666666666666"), new Guid("22222222-2222-7222-8222-222222222222") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("66666666-6666-7666-8666-666666666666"), new Guid("33333333-3333-7333-8333-333333333333") });

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-7555-8555-555555555555"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-7666-8666-666666666666"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-7222-8222-222222222222"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-7333-8333-333333333333"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-7444-8444-444444444444"));

            migrationBuilder.DropColumn(
                name: "Token",
                table: "Invitations");

            migrationBuilder.AddColumn<string>(
                name: "TokenHash",
                table: "Invitations",
                type: "character varying(512)",
                maxLength: 512,
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Description", "IsDeleted", "Name", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("019cd45e-7cf2-7f36-9595-2bd07dc9e25b"), new DateTime(2025, 10, 15, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("a0000000-0000-7000-8000-000000000000"), null, false, "Tenants.View", null, null },
                    { new Guid("019cd45e-7cfc-70e9-895b-4175ca7f5ee6"), new DateTime(2025, 10, 15, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("a0000000-0000-7000-8000-000000000000"), null, false, "Users.Delete", null, null },
                    { new Guid("019cd45e-7cfc-7144-85d7-a624389a5bda"), new DateTime(2025, 10, 15, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("a0000000-0000-7000-8000-000000000000"), null, false, "Rewards.View", null, null },
                    { new Guid("019cd45e-7cfc-7156-83d3-81d9e8951b34"), new DateTime(2025, 10, 15, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("a0000000-0000-7000-8000-000000000000"), null, false, "Invitations.Delete", null, null },
                    { new Guid("019cd45e-7cfc-717e-b9df-56b9565cc5e1"), new DateTime(2025, 10, 15, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("a0000000-0000-7000-8000-000000000000"), null, false, "Invitations.View", null, null },
                    { new Guid("019cd45e-7cfc-717f-853f-ed4d8c572e50"), new DateTime(2025, 10, 15, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("a0000000-0000-7000-8000-000000000000"), null, false, "Invitations.Edit", null, null },
                    { new Guid("019cd45e-7cfc-71fd-b726-7446578cf8b8"), new DateTime(2025, 10, 15, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("a0000000-0000-7000-8000-000000000000"), null, false, "Cards.View", null, null },
                    { new Guid("019cd45e-7cfc-72ca-892a-4c5484b063af"), new DateTime(2025, 10, 15, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("a0000000-0000-7000-8000-000000000000"), null, false, "Rewards.Delete", null, null },
                    { new Guid("019cd45e-7cfc-735f-ac03-e6b5ebe67f6e"), new DateTime(2025, 10, 15, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("a0000000-0000-7000-8000-000000000000"), null, false, "Scan.Redeem", null, null },
                    { new Guid("019cd45e-7cfc-739a-9ceb-77a6fae361a7"), new DateTime(2025, 10, 15, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("a0000000-0000-7000-8000-000000000000"), null, false, "Rewards.Edit", null, null },
                    { new Guid("019cd45e-7cfc-7499-a257-849fd24c2871"), new DateTime(2025, 10, 15, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("a0000000-0000-7000-8000-000000000000"), null, false, "Cards.Edit", null, null },
                    { new Guid("019cd45e-7cfc-75ec-9acf-136b67adda34"), new DateTime(2025, 10, 15, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("a0000000-0000-7000-8000-000000000000"), null, false, "Cards.Add", null, null },
                    { new Guid("019cd45e-7cfc-76d3-b56e-cc03d8854ff4"), new DateTime(2025, 10, 15, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("a0000000-0000-7000-8000-000000000000"), null, false, "Users.Edit", null, null },
                    { new Guid("019cd45e-7cfc-7807-8f5e-f4cce0b1ee3a"), new DateTime(2025, 10, 15, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("a0000000-0000-7000-8000-000000000000"), null, false, "Tenants.Delete", null, null },
                    { new Guid("019cd45e-7cfc-7839-a733-b7b1c30138d4"), new DateTime(2025, 10, 15, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("a0000000-0000-7000-8000-000000000000"), null, false, "Tenants.Edit", null, null },
                    { new Guid("019cd45e-7cfc-78ba-b9fc-d69399cdc202"), new DateTime(2025, 10, 15, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("a0000000-0000-7000-8000-000000000000"), null, false, "Scan.Stamping", null, null },
                    { new Guid("019cd45e-7cfc-78d6-8548-e20dbdadc04b"), new DateTime(2025, 10, 15, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("a0000000-0000-7000-8000-000000000000"), null, false, "Users.Add", null, null },
                    { new Guid("019cd45e-7cfc-7947-88e3-7c06f899920f"), new DateTime(2025, 10, 15, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("a0000000-0000-7000-8000-000000000000"), null, false, "Users.View", null, null },
                    { new Guid("019cd45e-7cfc-79ad-8fd5-cf99cd27ad06"), new DateTime(2025, 10, 15, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("a0000000-0000-7000-8000-000000000000"), null, false, "Cards.Delete", null, null },
                    { new Guid("019cd45e-7cfc-7af3-8e82-501087bdc3fd"), new DateTime(2025, 10, 15, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("a0000000-0000-7000-8000-000000000000"), null, false, "Rewards.Add", null, null },
                    { new Guid("019cd45e-7cfc-7ef1-9e1a-8ae8e3ff78f6"), new DateTime(2025, 10, 15, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("a0000000-0000-7000-8000-000000000000"), null, false, "Invitations.Add", null, null },
                    { new Guid("019cd45e-7cfc-7f11-9cdc-4581e5854eac"), new DateTime(2025, 10, 15, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("a0000000-0000-7000-8000-000000000000"), null, false, "Tenants.Add", null, null },
                    { new Guid("019cd465-18f5-7e38-951f-99444e306980"), new DateTime(2025, 10, 15, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("a0000000-0000-7000-8000-000000000000"), null, false, "System.Manage", null, null },
                    { new Guid("019cd465-18ff-733b-9eed-eaadb6650b4c"), new DateTime(2025, 10, 15, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("a0000000-0000-7000-8000-000000000000"), null, false, "Tenants.Manage", null, null },
                    { new Guid("019cd465-18ff-7481-be6f-34e04e75f169"), new DateTime(2025, 10, 15, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("a0000000-0000-7000-8000-000000000000"), null, false, "System.Audit.View", null, null },
                    { new Guid("019cd465-18ff-77ba-b57f-2026a084e84a"), new DateTime(2025, 10, 15, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("a0000000-0000-7000-8000-000000000000"), null, false, "System.Settings.Edit", null, null },
                    { new Guid("019cd465-18ff-7956-a606-0c8f350e2330"), new DateTime(2025, 10, 15, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("a0000000-0000-7000-8000-000000000000"), null, false, "System.Logs.View", null, null }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Description", "IsDeleted", "Name", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("019cd46a-80a8-76a2-b7eb-20ca5903c25e"), new DateTime(2025, 10, 15, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("a0000000-0000-7000-8000-000000000000"), "Full system administration and management.", false, "SuperAdmin", null, null },
                    { new Guid("019cd46a-80b3-712e-8b82-edbae70f6a0d"), new DateTime(2025, 10, 15, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("a0000000-0000-7000-8000-000000000000"), "Staff access for scanning and stamping loyalty cards.", false, "Merchant", null, null },
                    { new Guid("019cd46a-80b3-7d68-8834-46e510948741"), new DateTime(2025, 10, 15, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("a0000000-0000-7000-8000-000000000000"), "End-user access for viewing cards and rewards.", false, "User", null, null },
                    { new Guid("019cd46a-80b3-7eb0-9861-254e15e297db"), new DateTime(2025, 10, 15, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("a0000000-0000-7000-8000-000000000000"), "Administration of a specific tenant and its resources.", false, "TenantAdmin", null, null }
                });

            migrationBuilder.InsertData(
                table: "RolePermissions",
                columns: new[] { "PermissionId", "RoleId" },
                values: new object[,]
                {
                    { new Guid("019cd45e-7cf2-7f36-9595-2bd07dc9e25b"), new Guid("019cd46a-80a8-76a2-b7eb-20ca5903c25e") },
                    { new Guid("019cd45e-7cf2-7f36-9595-2bd07dc9e25b"), new Guid("019cd46a-80b3-712e-8b82-edbae70f6a0d") },
                    { new Guid("019cd45e-7cf2-7f36-9595-2bd07dc9e25b"), new Guid("019cd46a-80b3-7d68-8834-46e510948741") },
                    { new Guid("019cd45e-7cf2-7f36-9595-2bd07dc9e25b"), new Guid("019cd46a-80b3-7eb0-9861-254e15e297db") },
                    { new Guid("019cd45e-7cfc-70e9-895b-4175ca7f5ee6"), new Guid("019cd46a-80a8-76a2-b7eb-20ca5903c25e") },
                    { new Guid("019cd45e-7cfc-70e9-895b-4175ca7f5ee6"), new Guid("019cd46a-80b3-7eb0-9861-254e15e297db") },
                    { new Guid("019cd45e-7cfc-7144-85d7-a624389a5bda"), new Guid("019cd46a-80a8-76a2-b7eb-20ca5903c25e") },
                    { new Guid("019cd45e-7cfc-7144-85d7-a624389a5bda"), new Guid("019cd46a-80b3-712e-8b82-edbae70f6a0d") },
                    { new Guid("019cd45e-7cfc-7144-85d7-a624389a5bda"), new Guid("019cd46a-80b3-7d68-8834-46e510948741") },
                    { new Guid("019cd45e-7cfc-7144-85d7-a624389a5bda"), new Guid("019cd46a-80b3-7eb0-9861-254e15e297db") },
                    { new Guid("019cd45e-7cfc-7156-83d3-81d9e8951b34"), new Guid("019cd46a-80a8-76a2-b7eb-20ca5903c25e") },
                    { new Guid("019cd45e-7cfc-7156-83d3-81d9e8951b34"), new Guid("019cd46a-80b3-7eb0-9861-254e15e297db") },
                    { new Guid("019cd45e-7cfc-717e-b9df-56b9565cc5e1"), new Guid("019cd46a-80a8-76a2-b7eb-20ca5903c25e") },
                    { new Guid("019cd45e-7cfc-717e-b9df-56b9565cc5e1"), new Guid("019cd46a-80b3-7eb0-9861-254e15e297db") },
                    { new Guid("019cd45e-7cfc-717f-853f-ed4d8c572e50"), new Guid("019cd46a-80a8-76a2-b7eb-20ca5903c25e") },
                    { new Guid("019cd45e-7cfc-717f-853f-ed4d8c572e50"), new Guid("019cd46a-80b3-7eb0-9861-254e15e297db") },
                    { new Guid("019cd45e-7cfc-71fd-b726-7446578cf8b8"), new Guid("019cd46a-80a8-76a2-b7eb-20ca5903c25e") },
                    { new Guid("019cd45e-7cfc-71fd-b726-7446578cf8b8"), new Guid("019cd46a-80b3-712e-8b82-edbae70f6a0d") },
                    { new Guid("019cd45e-7cfc-71fd-b726-7446578cf8b8"), new Guid("019cd46a-80b3-7d68-8834-46e510948741") },
                    { new Guid("019cd45e-7cfc-71fd-b726-7446578cf8b8"), new Guid("019cd46a-80b3-7eb0-9861-254e15e297db") },
                    { new Guid("019cd45e-7cfc-72ca-892a-4c5484b063af"), new Guid("019cd46a-80a8-76a2-b7eb-20ca5903c25e") },
                    { new Guid("019cd45e-7cfc-72ca-892a-4c5484b063af"), new Guid("019cd46a-80b3-7eb0-9861-254e15e297db") },
                    { new Guid("019cd45e-7cfc-735f-ac03-e6b5ebe67f6e"), new Guid("019cd46a-80a8-76a2-b7eb-20ca5903c25e") },
                    { new Guid("019cd45e-7cfc-735f-ac03-e6b5ebe67f6e"), new Guid("019cd46a-80b3-712e-8b82-edbae70f6a0d") },
                    { new Guid("019cd45e-7cfc-735f-ac03-e6b5ebe67f6e"), new Guid("019cd46a-80b3-7eb0-9861-254e15e297db") },
                    { new Guid("019cd45e-7cfc-739a-9ceb-77a6fae361a7"), new Guid("019cd46a-80a8-76a2-b7eb-20ca5903c25e") },
                    { new Guid("019cd45e-7cfc-739a-9ceb-77a6fae361a7"), new Guid("019cd46a-80b3-7eb0-9861-254e15e297db") },
                    { new Guid("019cd45e-7cfc-7499-a257-849fd24c2871"), new Guid("019cd46a-80a8-76a2-b7eb-20ca5903c25e") },
                    { new Guid("019cd45e-7cfc-7499-a257-849fd24c2871"), new Guid("019cd46a-80b3-7eb0-9861-254e15e297db") },
                    { new Guid("019cd45e-7cfc-75ec-9acf-136b67adda34"), new Guid("019cd46a-80a8-76a2-b7eb-20ca5903c25e") },
                    { new Guid("019cd45e-7cfc-75ec-9acf-136b67adda34"), new Guid("019cd46a-80b3-7eb0-9861-254e15e297db") },
                    { new Guid("019cd45e-7cfc-76d3-b56e-cc03d8854ff4"), new Guid("019cd46a-80a8-76a2-b7eb-20ca5903c25e") },
                    { new Guid("019cd45e-7cfc-76d3-b56e-cc03d8854ff4"), new Guid("019cd46a-80b3-7eb0-9861-254e15e297db") },
                    { new Guid("019cd45e-7cfc-7807-8f5e-f4cce0b1ee3a"), new Guid("019cd46a-80a8-76a2-b7eb-20ca5903c25e") },
                    { new Guid("019cd45e-7cfc-7839-a733-b7b1c30138d4"), new Guid("019cd46a-80a8-76a2-b7eb-20ca5903c25e") },
                    { new Guid("019cd45e-7cfc-7839-a733-b7b1c30138d4"), new Guid("019cd46a-80b3-7eb0-9861-254e15e297db") },
                    { new Guid("019cd45e-7cfc-78ba-b9fc-d69399cdc202"), new Guid("019cd46a-80a8-76a2-b7eb-20ca5903c25e") },
                    { new Guid("019cd45e-7cfc-78ba-b9fc-d69399cdc202"), new Guid("019cd46a-80b3-712e-8b82-edbae70f6a0d") },
                    { new Guid("019cd45e-7cfc-78ba-b9fc-d69399cdc202"), new Guid("019cd46a-80b3-7eb0-9861-254e15e297db") },
                    { new Guid("019cd45e-7cfc-78d6-8548-e20dbdadc04b"), new Guid("019cd46a-80a8-76a2-b7eb-20ca5903c25e") },
                    { new Guid("019cd45e-7cfc-78d6-8548-e20dbdadc04b"), new Guid("019cd46a-80b3-7eb0-9861-254e15e297db") },
                    { new Guid("019cd45e-7cfc-7947-88e3-7c06f899920f"), new Guid("019cd46a-80a8-76a2-b7eb-20ca5903c25e") },
                    { new Guid("019cd45e-7cfc-7947-88e3-7c06f899920f"), new Guid("019cd46a-80b3-712e-8b82-edbae70f6a0d") },
                    { new Guid("019cd45e-7cfc-7947-88e3-7c06f899920f"), new Guid("019cd46a-80b3-7eb0-9861-254e15e297db") },
                    { new Guid("019cd45e-7cfc-79ad-8fd5-cf99cd27ad06"), new Guid("019cd46a-80a8-76a2-b7eb-20ca5903c25e") },
                    { new Guid("019cd45e-7cfc-79ad-8fd5-cf99cd27ad06"), new Guid("019cd46a-80b3-7eb0-9861-254e15e297db") },
                    { new Guid("019cd45e-7cfc-7af3-8e82-501087bdc3fd"), new Guid("019cd46a-80a8-76a2-b7eb-20ca5903c25e") },
                    { new Guid("019cd45e-7cfc-7af3-8e82-501087bdc3fd"), new Guid("019cd46a-80b3-7eb0-9861-254e15e297db") },
                    { new Guid("019cd45e-7cfc-7ef1-9e1a-8ae8e3ff78f6"), new Guid("019cd46a-80a8-76a2-b7eb-20ca5903c25e") },
                    { new Guid("019cd45e-7cfc-7ef1-9e1a-8ae8e3ff78f6"), new Guid("019cd46a-80b3-7eb0-9861-254e15e297db") },
                    { new Guid("019cd45e-7cfc-7f11-9cdc-4581e5854eac"), new Guid("019cd46a-80a8-76a2-b7eb-20ca5903c25e") },
                    { new Guid("019cd465-18f5-7e38-951f-99444e306980"), new Guid("019cd46a-80a8-76a2-b7eb-20ca5903c25e") },
                    { new Guid("019cd465-18ff-733b-9eed-eaadb6650b4c"), new Guid("019cd46a-80a8-76a2-b7eb-20ca5903c25e") },
                    { new Guid("019cd465-18ff-7481-be6f-34e04e75f169"), new Guid("019cd46a-80a8-76a2-b7eb-20ca5903c25e") },
                    { new Guid("019cd465-18ff-77ba-b57f-2026a084e84a"), new Guid("019cd46a-80a8-76a2-b7eb-20ca5903c25e") },
                    { new Guid("019cd465-18ff-7956-a606-0c8f350e2330"), new Guid("019cd46a-80a8-76a2-b7eb-20ca5903c25e") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Invitations_TokenHash",
                table: "Invitations",
                column: "TokenHash",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Invitations_TokenHash",
                table: "Invitations");

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("019cd45e-7cf2-7f36-9595-2bd07dc9e25b"), new Guid("019cd46a-80a8-76a2-b7eb-20ca5903c25e") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("019cd45e-7cf2-7f36-9595-2bd07dc9e25b"), new Guid("019cd46a-80b3-712e-8b82-edbae70f6a0d") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("019cd45e-7cf2-7f36-9595-2bd07dc9e25b"), new Guid("019cd46a-80b3-7d68-8834-46e510948741") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("019cd45e-7cf2-7f36-9595-2bd07dc9e25b"), new Guid("019cd46a-80b3-7eb0-9861-254e15e297db") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("019cd45e-7cfc-70e9-895b-4175ca7f5ee6"), new Guid("019cd46a-80a8-76a2-b7eb-20ca5903c25e") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("019cd45e-7cfc-70e9-895b-4175ca7f5ee6"), new Guid("019cd46a-80b3-7eb0-9861-254e15e297db") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("019cd45e-7cfc-7144-85d7-a624389a5bda"), new Guid("019cd46a-80a8-76a2-b7eb-20ca5903c25e") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("019cd45e-7cfc-7144-85d7-a624389a5bda"), new Guid("019cd46a-80b3-712e-8b82-edbae70f6a0d") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("019cd45e-7cfc-7144-85d7-a624389a5bda"), new Guid("019cd46a-80b3-7d68-8834-46e510948741") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("019cd45e-7cfc-7144-85d7-a624389a5bda"), new Guid("019cd46a-80b3-7eb0-9861-254e15e297db") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("019cd45e-7cfc-7156-83d3-81d9e8951b34"), new Guid("019cd46a-80a8-76a2-b7eb-20ca5903c25e") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("019cd45e-7cfc-7156-83d3-81d9e8951b34"), new Guid("019cd46a-80b3-7eb0-9861-254e15e297db") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("019cd45e-7cfc-717e-b9df-56b9565cc5e1"), new Guid("019cd46a-80a8-76a2-b7eb-20ca5903c25e") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("019cd45e-7cfc-717e-b9df-56b9565cc5e1"), new Guid("019cd46a-80b3-7eb0-9861-254e15e297db") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("019cd45e-7cfc-717f-853f-ed4d8c572e50"), new Guid("019cd46a-80a8-76a2-b7eb-20ca5903c25e") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("019cd45e-7cfc-717f-853f-ed4d8c572e50"), new Guid("019cd46a-80b3-7eb0-9861-254e15e297db") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("019cd45e-7cfc-71fd-b726-7446578cf8b8"), new Guid("019cd46a-80a8-76a2-b7eb-20ca5903c25e") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("019cd45e-7cfc-71fd-b726-7446578cf8b8"), new Guid("019cd46a-80b3-712e-8b82-edbae70f6a0d") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("019cd45e-7cfc-71fd-b726-7446578cf8b8"), new Guid("019cd46a-80b3-7d68-8834-46e510948741") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("019cd45e-7cfc-71fd-b726-7446578cf8b8"), new Guid("019cd46a-80b3-7eb0-9861-254e15e297db") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("019cd45e-7cfc-72ca-892a-4c5484b063af"), new Guid("019cd46a-80a8-76a2-b7eb-20ca5903c25e") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("019cd45e-7cfc-72ca-892a-4c5484b063af"), new Guid("019cd46a-80b3-7eb0-9861-254e15e297db") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("019cd45e-7cfc-735f-ac03-e6b5ebe67f6e"), new Guid("019cd46a-80a8-76a2-b7eb-20ca5903c25e") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("019cd45e-7cfc-735f-ac03-e6b5ebe67f6e"), new Guid("019cd46a-80b3-712e-8b82-edbae70f6a0d") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("019cd45e-7cfc-735f-ac03-e6b5ebe67f6e"), new Guid("019cd46a-80b3-7eb0-9861-254e15e297db") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("019cd45e-7cfc-739a-9ceb-77a6fae361a7"), new Guid("019cd46a-80a8-76a2-b7eb-20ca5903c25e") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("019cd45e-7cfc-739a-9ceb-77a6fae361a7"), new Guid("019cd46a-80b3-7eb0-9861-254e15e297db") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("019cd45e-7cfc-7499-a257-849fd24c2871"), new Guid("019cd46a-80a8-76a2-b7eb-20ca5903c25e") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("019cd45e-7cfc-7499-a257-849fd24c2871"), new Guid("019cd46a-80b3-7eb0-9861-254e15e297db") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("019cd45e-7cfc-75ec-9acf-136b67adda34"), new Guid("019cd46a-80a8-76a2-b7eb-20ca5903c25e") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("019cd45e-7cfc-75ec-9acf-136b67adda34"), new Guid("019cd46a-80b3-7eb0-9861-254e15e297db") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("019cd45e-7cfc-76d3-b56e-cc03d8854ff4"), new Guid("019cd46a-80a8-76a2-b7eb-20ca5903c25e") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("019cd45e-7cfc-76d3-b56e-cc03d8854ff4"), new Guid("019cd46a-80b3-7eb0-9861-254e15e297db") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("019cd45e-7cfc-7807-8f5e-f4cce0b1ee3a"), new Guid("019cd46a-80a8-76a2-b7eb-20ca5903c25e") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("019cd45e-7cfc-7839-a733-b7b1c30138d4"), new Guid("019cd46a-80a8-76a2-b7eb-20ca5903c25e") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("019cd45e-7cfc-7839-a733-b7b1c30138d4"), new Guid("019cd46a-80b3-7eb0-9861-254e15e297db") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("019cd45e-7cfc-78ba-b9fc-d69399cdc202"), new Guid("019cd46a-80a8-76a2-b7eb-20ca5903c25e") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("019cd45e-7cfc-78ba-b9fc-d69399cdc202"), new Guid("019cd46a-80b3-712e-8b82-edbae70f6a0d") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("019cd45e-7cfc-78ba-b9fc-d69399cdc202"), new Guid("019cd46a-80b3-7eb0-9861-254e15e297db") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("019cd45e-7cfc-78d6-8548-e20dbdadc04b"), new Guid("019cd46a-80a8-76a2-b7eb-20ca5903c25e") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("019cd45e-7cfc-78d6-8548-e20dbdadc04b"), new Guid("019cd46a-80b3-7eb0-9861-254e15e297db") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("019cd45e-7cfc-7947-88e3-7c06f899920f"), new Guid("019cd46a-80a8-76a2-b7eb-20ca5903c25e") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("019cd45e-7cfc-7947-88e3-7c06f899920f"), new Guid("019cd46a-80b3-712e-8b82-edbae70f6a0d") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("019cd45e-7cfc-7947-88e3-7c06f899920f"), new Guid("019cd46a-80b3-7eb0-9861-254e15e297db") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("019cd45e-7cfc-79ad-8fd5-cf99cd27ad06"), new Guid("019cd46a-80a8-76a2-b7eb-20ca5903c25e") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("019cd45e-7cfc-79ad-8fd5-cf99cd27ad06"), new Guid("019cd46a-80b3-7eb0-9861-254e15e297db") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("019cd45e-7cfc-7af3-8e82-501087bdc3fd"), new Guid("019cd46a-80a8-76a2-b7eb-20ca5903c25e") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("019cd45e-7cfc-7af3-8e82-501087bdc3fd"), new Guid("019cd46a-80b3-7eb0-9861-254e15e297db") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("019cd45e-7cfc-7ef1-9e1a-8ae8e3ff78f6"), new Guid("019cd46a-80a8-76a2-b7eb-20ca5903c25e") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("019cd45e-7cfc-7ef1-9e1a-8ae8e3ff78f6"), new Guid("019cd46a-80b3-7eb0-9861-254e15e297db") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("019cd45e-7cfc-7f11-9cdc-4581e5854eac"), new Guid("019cd46a-80a8-76a2-b7eb-20ca5903c25e") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("019cd465-18f5-7e38-951f-99444e306980"), new Guid("019cd46a-80a8-76a2-b7eb-20ca5903c25e") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("019cd465-18ff-733b-9eed-eaadb6650b4c"), new Guid("019cd46a-80a8-76a2-b7eb-20ca5903c25e") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("019cd465-18ff-7481-be6f-34e04e75f169"), new Guid("019cd46a-80a8-76a2-b7eb-20ca5903c25e") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("019cd465-18ff-77ba-b57f-2026a084e84a"), new Guid("019cd46a-80a8-76a2-b7eb-20ca5903c25e") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("019cd465-18ff-7956-a606-0c8f350e2330"), new Guid("019cd46a-80a8-76a2-b7eb-20ca5903c25e") });

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("019cd45e-7cf2-7f36-9595-2bd07dc9e25b"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("019cd45e-7cfc-70e9-895b-4175ca7f5ee6"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("019cd45e-7cfc-7144-85d7-a624389a5bda"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("019cd45e-7cfc-7156-83d3-81d9e8951b34"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("019cd45e-7cfc-717e-b9df-56b9565cc5e1"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("019cd45e-7cfc-717f-853f-ed4d8c572e50"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("019cd45e-7cfc-71fd-b726-7446578cf8b8"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("019cd45e-7cfc-72ca-892a-4c5484b063af"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("019cd45e-7cfc-735f-ac03-e6b5ebe67f6e"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("019cd45e-7cfc-739a-9ceb-77a6fae361a7"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("019cd45e-7cfc-7499-a257-849fd24c2871"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("019cd45e-7cfc-75ec-9acf-136b67adda34"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("019cd45e-7cfc-76d3-b56e-cc03d8854ff4"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("019cd45e-7cfc-7807-8f5e-f4cce0b1ee3a"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("019cd45e-7cfc-7839-a733-b7b1c30138d4"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("019cd45e-7cfc-78ba-b9fc-d69399cdc202"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("019cd45e-7cfc-78d6-8548-e20dbdadc04b"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("019cd45e-7cfc-7947-88e3-7c06f899920f"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("019cd45e-7cfc-79ad-8fd5-cf99cd27ad06"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("019cd45e-7cfc-7af3-8e82-501087bdc3fd"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("019cd45e-7cfc-7ef1-9e1a-8ae8e3ff78f6"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("019cd45e-7cfc-7f11-9cdc-4581e5854eac"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("019cd465-18f5-7e38-951f-99444e306980"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("019cd465-18ff-733b-9eed-eaadb6650b4c"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("019cd465-18ff-7481-be6f-34e04e75f169"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("019cd465-18ff-77ba-b57f-2026a084e84a"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("019cd465-18ff-7956-a606-0c8f350e2330"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("019cd46a-80a8-76a2-b7eb-20ca5903c25e"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("019cd46a-80b3-712e-8b82-edbae70f6a0d"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("019cd46a-80b3-7d68-8834-46e510948741"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("019cd46a-80b3-7eb0-9861-254e15e297db"));

            migrationBuilder.DropColumn(
                name: "TokenHash",
                table: "Invitations");

            migrationBuilder.AddColumn<string>(
                name: "Token",
                table: "Invitations",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Description", "IsDeleted", "Name", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("55555555-5555-7555-8555-555555555555"), new DateTime(2025, 10, 15, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("a0000000-0000-7000-8000-000000000000"), "some description", false, "User.Read", null, null },
                    { new Guid("66666666-6666-7666-8666-666666666666"), new DateTime(2025, 10, 15, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("a0000000-0000-7000-8000-000000000000"), null, false, "Post.Approve", null, null }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Description", "IsDeleted", "Name", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("22222222-2222-7222-8222-222222222222"), new DateTime(2025, 10, 15, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("a0000000-0000-7000-8000-000000000000"), "Full unrestricted access.", false, "SuperAdmin", null, null },
                    { new Guid("33333333-3333-7333-8333-333333333333"), new DateTime(2025, 10, 15, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("a0000000-0000-7000-8000-000000000000"), "General administrative access.", false, "Admin", null, null },
                    { new Guid("44444444-4444-7444-8444-444444444444"), new DateTime(2025, 10, 15, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("a0000000-0000-7000-8000-000000000000"), "Standard registered user access.", false, "User", null, null }
                });

            migrationBuilder.InsertData(
                table: "RolePermissions",
                columns: new[] { "PermissionId", "RoleId" },
                values: new object[,]
                {
                    { new Guid("55555555-5555-7555-8555-555555555555"), new Guid("22222222-2222-7222-8222-222222222222") },
                    { new Guid("55555555-5555-7555-8555-555555555555"), new Guid("33333333-3333-7333-8333-333333333333") },
                    { new Guid("55555555-5555-7555-8555-555555555555"), new Guid("44444444-4444-7444-8444-444444444444") },
                    { new Guid("66666666-6666-7666-8666-666666666666"), new Guid("22222222-2222-7222-8222-222222222222") },
                    { new Guid("66666666-6666-7666-8666-666666666666"), new Guid("33333333-3333-7333-8333-333333333333") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Invitations_Token",
                table: "Invitations",
                column: "Token",
                unique: true);
        }
    }
}
