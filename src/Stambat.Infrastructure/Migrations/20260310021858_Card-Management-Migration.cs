using System;

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Stambat.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CardManagementMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "WalletPass",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CardTemplateId = table.Column<Guid>(type: "uuid", nullable: false),
                    CurrentStamps = table.Column<int>(type: "integer", nullable: false),
                    ApplePassSerialNumber = table.Column<string>(type: "text", nullable: true),
                    GooglePayId = table.Column<string>(type: "text", nullable: true),
                    LastStampedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
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

            migrationBuilder.CreateIndex(
                name: "IX_CardTemplate_TenantId",
                table: "CardTemplate",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_StampTransaction_MerchantId",
                table: "StampTransaction",
                column: "MerchantId");

            migrationBuilder.CreateIndex(
                name: "IX_StampTransaction_WalletPassId",
                table: "StampTransaction",
                column: "WalletPassId");

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
                name: "StampTransaction");

            migrationBuilder.DropTable(
                name: "WalletPass");

            migrationBuilder.DropTable(
                name: "CardTemplate");
        }
    }
}
