using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Stambat.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class GoogleWalletIntegrationMigrationV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_WalletPasses_UserId_CardTemplateId",
                table: "WalletPasses");

            migrationBuilder.CreateIndex(
                name: "IX_WalletPasses_UserId_CardTemplateId",
                table: "WalletPasses",
                columns: new[] { "UserId", "CardTemplateId" },
                unique: true,
                filter: "\"Status\" IN (1, 2)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_WalletPasses_UserId_CardTemplateId",
                table: "WalletPasses");

            migrationBuilder.CreateIndex(
                name: "IX_WalletPasses_UserId_CardTemplateId",
                table: "WalletPasses",
                columns: new[] { "UserId", "CardTemplateId" },
                unique: true);
        }
    }
}
