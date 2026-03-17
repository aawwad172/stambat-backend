using System;

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Stambat.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FinalizingRBACFinalll : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Description", "IsDeleted", "Name", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new Guid("019cd47e-8346-7a3e-9c1c-f0d3a65d4091"), new DateTime(2025, 10, 15, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("a0000000-0000-7000-8000-000000000000"), null, false, "Tenants.Setup", null, null });

            migrationBuilder.InsertData(
                table: "RolePermissions",
                columns: new[] { "PermissionId", "RoleId" },
                values: new object[,]
                {
                    { new Guid("019cd47e-8346-7a3e-9c1c-f0d3a65d4091"), new Guid("019cd46a-80a8-76a2-b7eb-20ca5903c25e") },
                    { new Guid("019cd47e-8346-7a3e-9c1c-f0d3a65d4091"), new Guid("019cd46a-80b3-7d68-8834-46e510948741") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("019cd47e-8346-7a3e-9c1c-f0d3a65d4091"), new Guid("019cd46a-80a8-76a2-b7eb-20ca5903c25e") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("019cd47e-8346-7a3e-9c1c-f0d3a65d4091"), new Guid("019cd46a-80b3-7d68-8834-46e510948741") });

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("019cd47e-8346-7a3e-9c1c-f0d3a65d4091"));
        }
    }
}
