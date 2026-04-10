using System;

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Stambat.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddWalletQrIntegration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CardTemplate_Tenants_TenantId",
                table: "CardTemplate");

            migrationBuilder.DropForeignKey(
                name: "FK_StampTransaction_Users_MerchantId",
                table: "StampTransaction");

            migrationBuilder.DropForeignKey(
                name: "FK_StampTransaction_WalletPass_WalletPassId",
                table: "StampTransaction");

            migrationBuilder.DropForeignKey(
                name: "FK_WalletPass_CardTemplate_CardTemplateId",
                table: "WalletPass");

            migrationBuilder.DropForeignKey(
                name: "FK_WalletPass_Users_UserId",
                table: "WalletPass");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WalletPass",
                table: "WalletPass");

            migrationBuilder.DropIndex(
                name: "IX_WalletPass_UserId",
                table: "WalletPass");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StampTransaction",
                table: "StampTransaction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CardTemplate",
                table: "CardTemplate");

            migrationBuilder.DropIndex(
                name: "IX_CardTemplate_TenantId",
                table: "CardTemplate");

            migrationBuilder.RenameTable(
                name: "WalletPass",
                newName: "WalletPasses");

            migrationBuilder.RenameTable(
                name: "StampTransaction",
                newName: "StampTransactions");

            migrationBuilder.RenameTable(
                name: "CardTemplate",
                newName: "CardTemplates");

            migrationBuilder.RenameIndex(
                name: "IX_WalletPass_CardTemplateId",
                table: "WalletPasses",
                newName: "IX_WalletPasses_CardTemplateId");

            migrationBuilder.RenameIndex(
                name: "IX_StampTransaction_WalletPassId",
                table: "StampTransactions",
                newName: "IX_StampTransactions_WalletPassId");

            migrationBuilder.RenameIndex(
                name: "IX_StampTransaction_MerchantId",
                table: "StampTransactions",
                newName: "IX_StampTransactions_MerchantId");

            migrationBuilder.AlterColumn<string>(
                name: "GooglePayId",
                table: "WalletPasses",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CurrentStamps",
                table: "WalletPasses",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "ApplePassSerialNumber",
                table: "WalletPasses",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "WalletPasses",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ProviderType",
                table: "WalletPasses",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "QrTokenPayload",
                table: "WalletPasses",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RedeemedAt",
                table: "WalletPasses",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RedemptionCount",
                table: "WalletPasses",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "WalletPasses",
                type: "integer",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "WalletPasses",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedBy",
                table: "WalletPasses",
                type: "uuid",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "StampsAdded",
                table: "StampTransactions",
                type: "integer",
                nullable: false,
                defaultValue: 1,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "Note",
                table: "StampTransactions",
                type: "character varying(250)",
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "StampTransactions",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "StampTransactions",
                type: "integer",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "CardTemplates",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "TermsAndConditions",
                table: "CardTemplates",
                type: "character varying(2000)",
                maxLength: 2000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SecondaryColorOverride",
                table: "CardTemplates",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RewardDescription",
                table: "CardTemplates",
                type: "character varying(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PrimaryColorOverride",
                table: "CardTemplates",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LogoUrlOverride",
                table: "CardTemplates",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EmptyStampUrl",
                table: "CardTemplates",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EarnedStampUrl",
                table: "CardTemplates",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "CardTemplates",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "JoinQrCodeBase64",
                table: "CardTemplates",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "JoinUrl",
                table: "CardTemplates",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WalletClassId",
                table: "CardTemplates",
                type: "character varying(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_WalletPasses",
                table: "WalletPasses",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StampTransactions",
                table: "StampTransactions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CardTemplates",
                table: "CardTemplates",
                column: "Id");

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Description", "IsDeleted", "Name", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("01966b3a-1c00-7a01-b5e8-4a3f6d8c9e12"), new DateTime(2025, 10, 15, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("a0000000-0000-7000-8000-000000000000"), null, false, "WalletPass.View", null, null },
                    { new Guid("01966b3a-1c00-7b02-c6f9-5b4e7e9d0f23"), new DateTime(2025, 10, 15, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("a0000000-0000-7000-8000-000000000000"), null, false, "WalletPass.Create", null, null }
                });

            migrationBuilder.InsertData(
                table: "RolePermissions",
                columns: new[] { "PermissionId", "RoleId" },
                values: new object[,]
                {
                    { new Guid("01966b3a-1c00-7a01-b5e8-4a3f6d8c9e12"), new Guid("019cd46a-80a8-76a2-b7eb-20ca5903c25e") },
                    { new Guid("01966b3a-1c00-7a01-b5e8-4a3f6d8c9e12"), new Guid("019cd46a-80b3-712e-8b82-edbae70f6a0d") },
                    { new Guid("01966b3a-1c00-7a01-b5e8-4a3f6d8c9e12"), new Guid("019cd46a-80b3-7a1a-a1b1-254e15e297db") },
                    { new Guid("01966b3a-1c00-7a01-b5e8-4a3f6d8c9e12"), new Guid("019cd46a-80b3-7eb0-9861-254e15e297db") },
                    { new Guid("01966b3a-1c00-7b02-c6f9-5b4e7e9d0f23"), new Guid("019cd46a-80a8-76a2-b7eb-20ca5903c25e") },
                    { new Guid("01966b3a-1c00-7b02-c6f9-5b4e7e9d0f23"), new Guid("019cd46a-80b3-712e-8b82-edbae70f6a0d") },
                    { new Guid("01966b3a-1c00-7b02-c6f9-5b4e7e9d0f23"), new Guid("019cd46a-80b3-7a1a-a1b1-254e15e297db") },
                    { new Guid("01966b3a-1c00-7b02-c6f9-5b4e7e9d0f23"), new Guid("019cd46a-80b3-7eb0-9861-254e15e297db") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_WalletPasses_UserId_CardTemplateId",
                table: "WalletPasses",
                columns: new[] { "UserId", "CardTemplateId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CardTemplates_TenantId_Title",
                table: "CardTemplates",
                columns: new[] { "TenantId", "Title" },
                unique: true,
                filter: "\"IsDeleted\" = false");

            migrationBuilder.AddForeignKey(
                name: "FK_CardTemplates_Tenants_TenantId",
                table: "CardTemplates",
                column: "TenantId",
                principalTable: "Tenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StampTransactions_Users_MerchantId",
                table: "StampTransactions",
                column: "MerchantId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StampTransactions_WalletPasses_WalletPassId",
                table: "StampTransactions",
                column: "WalletPassId",
                principalTable: "WalletPasses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WalletPasses_CardTemplates_CardTemplateId",
                table: "WalletPasses",
                column: "CardTemplateId",
                principalTable: "CardTemplates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WalletPasses_Users_UserId",
                table: "WalletPasses",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CardTemplates_Tenants_TenantId",
                table: "CardTemplates");

            migrationBuilder.DropForeignKey(
                name: "FK_StampTransactions_Users_MerchantId",
                table: "StampTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_StampTransactions_WalletPasses_WalletPassId",
                table: "StampTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_WalletPasses_CardTemplates_CardTemplateId",
                table: "WalletPasses");

            migrationBuilder.DropForeignKey(
                name: "FK_WalletPasses_Users_UserId",
                table: "WalletPasses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WalletPasses",
                table: "WalletPasses");

            migrationBuilder.DropIndex(
                name: "IX_WalletPasses_UserId_CardTemplateId",
                table: "WalletPasses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StampTransactions",
                table: "StampTransactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CardTemplates",
                table: "CardTemplates");

            migrationBuilder.DropIndex(
                name: "IX_CardTemplates_TenantId_Title",
                table: "CardTemplates");

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("01966b3a-1c00-7a01-b5e8-4a3f6d8c9e12"), new Guid("019cd46a-80a8-76a2-b7eb-20ca5903c25e") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("01966b3a-1c00-7a01-b5e8-4a3f6d8c9e12"), new Guid("019cd46a-80b3-712e-8b82-edbae70f6a0d") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("01966b3a-1c00-7a01-b5e8-4a3f6d8c9e12"), new Guid("019cd46a-80b3-7a1a-a1b1-254e15e297db") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("01966b3a-1c00-7a01-b5e8-4a3f6d8c9e12"), new Guid("019cd46a-80b3-7eb0-9861-254e15e297db") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("01966b3a-1c00-7b02-c6f9-5b4e7e9d0f23"), new Guid("019cd46a-80a8-76a2-b7eb-20ca5903c25e") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("01966b3a-1c00-7b02-c6f9-5b4e7e9d0f23"), new Guid("019cd46a-80b3-712e-8b82-edbae70f6a0d") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("01966b3a-1c00-7b02-c6f9-5b4e7e9d0f23"), new Guid("019cd46a-80b3-7a1a-a1b1-254e15e297db") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("01966b3a-1c00-7b02-c6f9-5b4e7e9d0f23"), new Guid("019cd46a-80b3-7eb0-9861-254e15e297db") });

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("01966b3a-1c00-7a01-b5e8-4a3f6d8c9e12"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("01966b3a-1c00-7b02-c6f9-5b4e7e9d0f23"));

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "WalletPasses");

            migrationBuilder.DropColumn(
                name: "ProviderType",
                table: "WalletPasses");

            migrationBuilder.DropColumn(
                name: "QrTokenPayload",
                table: "WalletPasses");

            migrationBuilder.DropColumn(
                name: "RedeemedAt",
                table: "WalletPasses");

            migrationBuilder.DropColumn(
                name: "RedemptionCount",
                table: "WalletPasses");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "WalletPasses");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "WalletPasses");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "WalletPasses");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "StampTransactions");

            migrationBuilder.DropColumn(
                name: "JoinQrCodeBase64",
                table: "CardTemplates");

            migrationBuilder.DropColumn(
                name: "JoinUrl",
                table: "CardTemplates");

            migrationBuilder.DropColumn(
                name: "WalletClassId",
                table: "CardTemplates");

            migrationBuilder.RenameTable(
                name: "WalletPasses",
                newName: "WalletPass");

            migrationBuilder.RenameTable(
                name: "StampTransactions",
                newName: "StampTransaction");

            migrationBuilder.RenameTable(
                name: "CardTemplates",
                newName: "CardTemplate");

            migrationBuilder.RenameIndex(
                name: "IX_WalletPasses_CardTemplateId",
                table: "WalletPass",
                newName: "IX_WalletPass_CardTemplateId");

            migrationBuilder.RenameIndex(
                name: "IX_StampTransactions_WalletPassId",
                table: "StampTransaction",
                newName: "IX_StampTransaction_WalletPassId");

            migrationBuilder.RenameIndex(
                name: "IX_StampTransactions_MerchantId",
                table: "StampTransaction",
                newName: "IX_StampTransaction_MerchantId");

            migrationBuilder.AlterColumn<string>(
                name: "GooglePayId",
                table: "WalletPass",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CurrentStamps",
                table: "WalletPass",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldDefaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "ApplePassSerialNumber",
                table: "WalletPass",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "StampsAdded",
                table: "StampTransaction",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldDefaultValue: 1);

            migrationBuilder.AlterColumn<string>(
                name: "Note",
                table: "StampTransaction",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(250)",
                oldMaxLength: 250,
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "StampTransaction",
                type: "boolean",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "CardTemplate",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "TermsAndConditions",
                table: "CardTemplate",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(2000)",
                oldMaxLength: 2000,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SecondaryColorOverride",
                table: "CardTemplate",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RewardDescription",
                table: "CardTemplate",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PrimaryColorOverride",
                table: "CardTemplate",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LogoUrlOverride",
                table: "CardTemplate",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EmptyStampUrl",
                table: "CardTemplate",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EarnedStampUrl",
                table: "CardTemplate",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "CardTemplate",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_WalletPass",
                table: "WalletPass",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StampTransaction",
                table: "StampTransaction",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CardTemplate",
                table: "CardTemplate",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_WalletPass_UserId",
                table: "WalletPass",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CardTemplate_TenantId",
                table: "CardTemplate",
                column: "TenantId");

            migrationBuilder.AddForeignKey(
                name: "FK_CardTemplate_Tenants_TenantId",
                table: "CardTemplate",
                column: "TenantId",
                principalTable: "Tenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StampTransaction_Users_MerchantId",
                table: "StampTransaction",
                column: "MerchantId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StampTransaction_WalletPass_WalletPassId",
                table: "StampTransaction",
                column: "WalletPassId",
                principalTable: "WalletPass",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WalletPass_CardTemplate_CardTemplateId",
                table: "WalletPass",
                column: "CardTemplateId",
                principalTable: "CardTemplate",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WalletPass_Users_UserId",
                table: "WalletPass",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
