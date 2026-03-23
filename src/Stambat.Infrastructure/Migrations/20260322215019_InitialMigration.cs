using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Stambat.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tenants",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BusinessName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    TenantProfileId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    TimeZoneId = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValue: "Asia/Amman"),
                    CurrencyCode = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false, defaultValue: "JOD"),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FullName_FirstName = table.Column<string>(type: "text", nullable: false),
                    FullName_MiddleName = table.Column<string>(type: "text", nullable: true),
                    FullName_LastName = table.Column<string>(type: "text", nullable: false),
                    Username = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    UserCredentialsId = table.Column<Guid>(type: "uuid", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    IsVerified = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RolePermissions",
                columns: table => new
                {
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    PermissionId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermissions", x => new { x.PermissionId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_RolePermissions_Permissions_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "Permissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolePermissions_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CardTemplate",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    StampsRequired = table.Column<int>(type: "integer", nullable: false),
                    RewardDescription = table.Column<string>(type: "text", nullable: true),
                    PrimaryColorOverride = table.Column<string>(type: "text", nullable: true),
                    SecondaryColorOverride = table.Column<string>(type: "text", nullable: true),
                    LogoUrlOverride = table.Column<string>(type: "text", nullable: true),
                    EmptyStampUrl = table.Column<string>(type: "text", nullable: true),
                    EarnedStampUrl = table.Column<string>(type: "text", nullable: true),
                    TermsAndConditions = table.Column<string>(type: "text", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardTemplate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CardTemplate_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Invitations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    TokenHash = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsUsed = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invitations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invitations_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Invitations_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "TenantProfiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Slug = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    LogoUrl = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    PrimaryColor = table.Column<string>(type: "character varying(7)", maxLength: 7, nullable: false, defaultValue: "#000000"),
                    SecondaryColor = table.Column<string>(type: "character varying(7)", maxLength: 7, nullable: false, defaultValue: "#FFFFFF"),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TenantProfiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TenantProfiles_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    TokenFamilyId = table.Column<Guid>(type: "uuid", nullable: false),
                    TokenHash = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    RevokedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ReasonRevoked = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    ReplacedByTokenId = table.Column<Guid>(type: "uuid", nullable: true),
                    SecurityStampAtIssue = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_RefreshTokens_ReplacedByTokenId",
                        column: x => x.ReplacedByTokenId,
                        principalTable: "RefreshTokens",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RefreshTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserCredentials",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PasswordHash = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCredentials", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserCredentials_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoleTenants",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoleTenants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRoleTenants_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoleTenants_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoleTenants_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTokens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Token = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsUsed = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WalletPass",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CardTemplateId = table.Column<Guid>(type: "uuid", nullable: false),
                    CurrentStamps = table.Column<int>(type: "integer", nullable: false),
                    ApplePassSerialNumber = table.Column<string>(type: "text", nullable: true),
                    GooglePayId = table.Column<string>(type: "text", nullable: true),
                    LastStampedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WalletPass", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WalletPass_CardTemplate_CardTemplateId",
                        column: x => x.CardTemplateId,
                        principalTable: "CardTemplate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WalletPass_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StampTransaction",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    WalletPassId = table.Column<Guid>(type: "uuid", nullable: false),
                    MerchantId = table.Column<Guid>(type: "uuid", nullable: false),
                    StampsAdded = table.Column<int>(type: "integer", nullable: false),
                    Note = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StampTransaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StampTransaction_Users_MerchantId",
                        column: x => x.MerchantId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StampTransaction_WalletPass_WalletPassId",
                        column: x => x.WalletPassId,
                        principalTable: "WalletPass",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                    { new Guid("019cd465-18ff-7956-a606-0c8f350e2330"), new DateTime(2025, 10, 15, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("a0000000-0000-7000-8000-000000000000"), null, false, "System.Logs.View", null, null },
                    { new Guid("019cd47e-8346-7a3e-9c1c-f0d3a65d4091"), new DateTime(2025, 10, 15, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("a0000000-0000-7000-8000-000000000000"), null, false, "Tenants.Setup", null, null }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Description", "IsDeleted", "Name", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("019cd46a-80a8-76a2-b7eb-20ca5903c25e"), new DateTime(2025, 10, 15, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("a0000000-0000-7000-8000-000000000000"), "Full system administration and management.", false, "SuperAdmin", null, null },
                    { new Guid("019cd46a-80b3-712e-8b82-edbae70f6a0d"), new DateTime(2025, 10, 15, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("a0000000-0000-7000-8000-000000000000"), "Staff access for scanning and stamping loyalty cards.", false, "Merchant", null, null },
                    { new Guid("019cd46a-80b3-7a1a-a1b1-254e15e297db"), new DateTime(2025, 10, 15, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("a0000000-0000-7000-8000-000000000000"), "Middle management for specific tenant features and staff management.", false, "Manager", null, null },
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
                    { new Guid("019cd45e-7cf2-7f36-9595-2bd07dc9e25b"), new Guid("019cd46a-80b3-7a1a-a1b1-254e15e297db") },
                    { new Guid("019cd45e-7cf2-7f36-9595-2bd07dc9e25b"), new Guid("019cd46a-80b3-7d68-8834-46e510948741") },
                    { new Guid("019cd45e-7cf2-7f36-9595-2bd07dc9e25b"), new Guid("019cd46a-80b3-7eb0-9861-254e15e297db") },
                    { new Guid("019cd45e-7cfc-70e9-895b-4175ca7f5ee6"), new Guid("019cd46a-80a8-76a2-b7eb-20ca5903c25e") },
                    { new Guid("019cd45e-7cfc-70e9-895b-4175ca7f5ee6"), new Guid("019cd46a-80b3-7eb0-9861-254e15e297db") },
                    { new Guid("019cd45e-7cfc-7144-85d7-a624389a5bda"), new Guid("019cd46a-80a8-76a2-b7eb-20ca5903c25e") },
                    { new Guid("019cd45e-7cfc-7144-85d7-a624389a5bda"), new Guid("019cd46a-80b3-712e-8b82-edbae70f6a0d") },
                    { new Guid("019cd45e-7cfc-7144-85d7-a624389a5bda"), new Guid("019cd46a-80b3-7a1a-a1b1-254e15e297db") },
                    { new Guid("019cd45e-7cfc-7144-85d7-a624389a5bda"), new Guid("019cd46a-80b3-7d68-8834-46e510948741") },
                    { new Guid("019cd45e-7cfc-7144-85d7-a624389a5bda"), new Guid("019cd46a-80b3-7eb0-9861-254e15e297db") },
                    { new Guid("019cd45e-7cfc-7156-83d3-81d9e8951b34"), new Guid("019cd46a-80a8-76a2-b7eb-20ca5903c25e") },
                    { new Guid("019cd45e-7cfc-7156-83d3-81d9e8951b34"), new Guid("019cd46a-80b3-7eb0-9861-254e15e297db") },
                    { new Guid("019cd45e-7cfc-717e-b9df-56b9565cc5e1"), new Guid("019cd46a-80a8-76a2-b7eb-20ca5903c25e") },
                    { new Guid("019cd45e-7cfc-717e-b9df-56b9565cc5e1"), new Guid("019cd46a-80b3-7a1a-a1b1-254e15e297db") },
                    { new Guid("019cd45e-7cfc-717e-b9df-56b9565cc5e1"), new Guid("019cd46a-80b3-7eb0-9861-254e15e297db") },
                    { new Guid("019cd45e-7cfc-717f-853f-ed4d8c572e50"), new Guid("019cd46a-80a8-76a2-b7eb-20ca5903c25e") },
                    { new Guid("019cd45e-7cfc-717f-853f-ed4d8c572e50"), new Guid("019cd46a-80b3-7eb0-9861-254e15e297db") },
                    { new Guid("019cd45e-7cfc-71fd-b726-7446578cf8b8"), new Guid("019cd46a-80a8-76a2-b7eb-20ca5903c25e") },
                    { new Guid("019cd45e-7cfc-71fd-b726-7446578cf8b8"), new Guid("019cd46a-80b3-712e-8b82-edbae70f6a0d") },
                    { new Guid("019cd45e-7cfc-71fd-b726-7446578cf8b8"), new Guid("019cd46a-80b3-7a1a-a1b1-254e15e297db") },
                    { new Guid("019cd45e-7cfc-71fd-b726-7446578cf8b8"), new Guid("019cd46a-80b3-7d68-8834-46e510948741") },
                    { new Guid("019cd45e-7cfc-71fd-b726-7446578cf8b8"), new Guid("019cd46a-80b3-7eb0-9861-254e15e297db") },
                    { new Guid("019cd45e-7cfc-72ca-892a-4c5484b063af"), new Guid("019cd46a-80a8-76a2-b7eb-20ca5903c25e") },
                    { new Guid("019cd45e-7cfc-72ca-892a-4c5484b063af"), new Guid("019cd46a-80b3-7eb0-9861-254e15e297db") },
                    { new Guid("019cd45e-7cfc-735f-ac03-e6b5ebe67f6e"), new Guid("019cd46a-80a8-76a2-b7eb-20ca5903c25e") },
                    { new Guid("019cd45e-7cfc-735f-ac03-e6b5ebe67f6e"), new Guid("019cd46a-80b3-712e-8b82-edbae70f6a0d") },
                    { new Guid("019cd45e-7cfc-735f-ac03-e6b5ebe67f6e"), new Guid("019cd46a-80b3-7a1a-a1b1-254e15e297db") },
                    { new Guid("019cd45e-7cfc-735f-ac03-e6b5ebe67f6e"), new Guid("019cd46a-80b3-7eb0-9861-254e15e297db") },
                    { new Guid("019cd45e-7cfc-739a-9ceb-77a6fae361a7"), new Guid("019cd46a-80a8-76a2-b7eb-20ca5903c25e") },
                    { new Guid("019cd45e-7cfc-739a-9ceb-77a6fae361a7"), new Guid("019cd46a-80b3-7eb0-9861-254e15e297db") },
                    { new Guid("019cd45e-7cfc-7499-a257-849fd24c2871"), new Guid("019cd46a-80a8-76a2-b7eb-20ca5903c25e") },
                    { new Guid("019cd45e-7cfc-7499-a257-849fd24c2871"), new Guid("019cd46a-80b3-7eb0-9861-254e15e297db") },
                    { new Guid("019cd45e-7cfc-75ec-9acf-136b67adda34"), new Guid("019cd46a-80a8-76a2-b7eb-20ca5903c25e") },
                    { new Guid("019cd45e-7cfc-75ec-9acf-136b67adda34"), new Guid("019cd46a-80b3-7a1a-a1b1-254e15e297db") },
                    { new Guid("019cd45e-7cfc-75ec-9acf-136b67adda34"), new Guid("019cd46a-80b3-7eb0-9861-254e15e297db") },
                    { new Guid("019cd45e-7cfc-76d3-b56e-cc03d8854ff4"), new Guid("019cd46a-80a8-76a2-b7eb-20ca5903c25e") },
                    { new Guid("019cd45e-7cfc-76d3-b56e-cc03d8854ff4"), new Guid("019cd46a-80b3-7eb0-9861-254e15e297db") },
                    { new Guid("019cd45e-7cfc-7807-8f5e-f4cce0b1ee3a"), new Guid("019cd46a-80a8-76a2-b7eb-20ca5903c25e") },
                    { new Guid("019cd45e-7cfc-7839-a733-b7b1c30138d4"), new Guid("019cd46a-80a8-76a2-b7eb-20ca5903c25e") },
                    { new Guid("019cd45e-7cfc-7839-a733-b7b1c30138d4"), new Guid("019cd46a-80b3-7eb0-9861-254e15e297db") },
                    { new Guid("019cd45e-7cfc-78ba-b9fc-d69399cdc202"), new Guid("019cd46a-80a8-76a2-b7eb-20ca5903c25e") },
                    { new Guid("019cd45e-7cfc-78ba-b9fc-d69399cdc202"), new Guid("019cd46a-80b3-712e-8b82-edbae70f6a0d") },
                    { new Guid("019cd45e-7cfc-78ba-b9fc-d69399cdc202"), new Guid("019cd46a-80b3-7a1a-a1b1-254e15e297db") },
                    { new Guid("019cd45e-7cfc-78ba-b9fc-d69399cdc202"), new Guid("019cd46a-80b3-7eb0-9861-254e15e297db") },
                    { new Guid("019cd45e-7cfc-78d6-8548-e20dbdadc04b"), new Guid("019cd46a-80a8-76a2-b7eb-20ca5903c25e") },
                    { new Guid("019cd45e-7cfc-78d6-8548-e20dbdadc04b"), new Guid("019cd46a-80b3-7a1a-a1b1-254e15e297db") },
                    { new Guid("019cd45e-7cfc-78d6-8548-e20dbdadc04b"), new Guid("019cd46a-80b3-7eb0-9861-254e15e297db") },
                    { new Guid("019cd45e-7cfc-7947-88e3-7c06f899920f"), new Guid("019cd46a-80a8-76a2-b7eb-20ca5903c25e") },
                    { new Guid("019cd45e-7cfc-7947-88e3-7c06f899920f"), new Guid("019cd46a-80b3-712e-8b82-edbae70f6a0d") },
                    { new Guid("019cd45e-7cfc-7947-88e3-7c06f899920f"), new Guid("019cd46a-80b3-7a1a-a1b1-254e15e297db") },
                    { new Guid("019cd45e-7cfc-7947-88e3-7c06f899920f"), new Guid("019cd46a-80b3-7eb0-9861-254e15e297db") },
                    { new Guid("019cd45e-7cfc-79ad-8fd5-cf99cd27ad06"), new Guid("019cd46a-80a8-76a2-b7eb-20ca5903c25e") },
                    { new Guid("019cd45e-7cfc-79ad-8fd5-cf99cd27ad06"), new Guid("019cd46a-80b3-7eb0-9861-254e15e297db") },
                    { new Guid("019cd45e-7cfc-7af3-8e82-501087bdc3fd"), new Guid("019cd46a-80a8-76a2-b7eb-20ca5903c25e") },
                    { new Guid("019cd45e-7cfc-7af3-8e82-501087bdc3fd"), new Guid("019cd46a-80b3-7eb0-9861-254e15e297db") },
                    { new Guid("019cd45e-7cfc-7ef1-9e1a-8ae8e3ff78f6"), new Guid("019cd46a-80a8-76a2-b7eb-20ca5903c25e") },
                    { new Guid("019cd45e-7cfc-7ef1-9e1a-8ae8e3ff78f6"), new Guid("019cd46a-80b3-7a1a-a1b1-254e15e297db") },
                    { new Guid("019cd45e-7cfc-7ef1-9e1a-8ae8e3ff78f6"), new Guid("019cd46a-80b3-7eb0-9861-254e15e297db") },
                    { new Guid("019cd45e-7cfc-7f11-9cdc-4581e5854eac"), new Guid("019cd46a-80a8-76a2-b7eb-20ca5903c25e") },
                    { new Guid("019cd465-18f5-7e38-951f-99444e306980"), new Guid("019cd46a-80a8-76a2-b7eb-20ca5903c25e") },
                    { new Guid("019cd465-18ff-733b-9eed-eaadb6650b4c"), new Guid("019cd46a-80a8-76a2-b7eb-20ca5903c25e") },
                    { new Guid("019cd465-18ff-7481-be6f-34e04e75f169"), new Guid("019cd46a-80a8-76a2-b7eb-20ca5903c25e") },
                    { new Guid("019cd465-18ff-77ba-b57f-2026a084e84a"), new Guid("019cd46a-80a8-76a2-b7eb-20ca5903c25e") },
                    { new Guid("019cd465-18ff-7956-a606-0c8f350e2330"), new Guid("019cd46a-80a8-76a2-b7eb-20ca5903c25e") },
                    { new Guid("019cd47e-8346-7a3e-9c1c-f0d3a65d4091"), new Guid("019cd46a-80a8-76a2-b7eb-20ca5903c25e") },
                    { new Guid("019cd47e-8346-7a3e-9c1c-f0d3a65d4091"), new Guid("019cd46a-80b3-7d68-8834-46e510948741") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CardTemplate_TenantId",
                table: "CardTemplate",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Invitations_Email_TenantId_RoleId",
                table: "Invitations",
                columns: new[] { "Email", "TenantId", "RoleId" },
                unique: true,
                filter: "\"IsUsed\" = false");

            migrationBuilder.CreateIndex(
                name: "IX_Invitations_RoleId",
                table: "Invitations",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Invitations_TenantId",
                table: "Invitations",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Invitations_TokenHash",
                table: "Invitations",
                column: "TokenHash",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_Name",
                table: "Permissions",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_ExpiresAt",
                table: "RefreshTokens",
                column: "ExpiresAt");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_ReplacedByTokenId",
                table: "RefreshTokens",
                column: "ReplacedByTokenId");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_TokenHash",
                table: "RefreshTokens",
                column: "TokenHash",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UserId",
                table: "RefreshTokens",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UserId_TokenFamilyId",
                table: "RefreshTokens",
                columns: new[] { "UserId", "TokenFamilyId" });

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_RoleId",
                table: "RolePermissions",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_Name",
                table: "Roles",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StampTransaction_MerchantId",
                table: "StampTransaction",
                column: "MerchantId");

            migrationBuilder.CreateIndex(
                name: "IX_StampTransaction_WalletPassId",
                table: "StampTransaction",
                column: "WalletPassId");

            migrationBuilder.CreateIndex(
                name: "IX_TenantProfiles_Slug",
                table: "TenantProfiles",
                column: "Slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TenantProfiles_TenantId",
                table: "TenantProfiles",
                column: "TenantId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tenants_Email",
                table: "Tenants",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserCredentials_UserId",
                table: "UserCredentials",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserRoleTenants_RoleId",
                table: "UserRoleTenants",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoleTenants_TenantId",
                table: "UserRoleTenants",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoleTenants_UserId_RoleId",
                table: "UserRoleTenants",
                columns: new[] { "UserId", "RoleId" },
                unique: true,
                filter: "\"TenantId\" IS NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoleTenants_UserId_RoleId_TenantId",
                table: "UserRoleTenants",
                columns: new[] { "UserId", "RoleId", "TenantId" },
                unique: true,
                filter: "\"TenantId\" IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserTokens_Token",
                table: "UserTokens",
                column: "Token",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserTokens_UserId",
                table: "UserTokens",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_WalletPass_CardTemplateId",
                table: "WalletPass",
                column: "CardTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_WalletPass_UserId",
                table: "WalletPass",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Invitations");

            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DropTable(
                name: "RolePermissions");

            migrationBuilder.DropTable(
                name: "StampTransaction");

            migrationBuilder.DropTable(
                name: "TenantProfiles");

            migrationBuilder.DropTable(
                name: "UserCredentials");

            migrationBuilder.DropTable(
                name: "UserRoleTenants");

            migrationBuilder.DropTable(
                name: "UserTokens");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "WalletPass");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "CardTemplate");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Tenants");
        }
    }
}
