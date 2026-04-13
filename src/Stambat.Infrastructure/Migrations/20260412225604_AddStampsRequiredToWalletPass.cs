using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Stambat.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddStampsRequiredToWalletPass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StampsRequired",
                table: "WalletPasses",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            // Backfill existing passes with the StampsRequired from their CardTemplate
            migrationBuilder.Sql("""
                UPDATE "WalletPasses" wp
                SET "StampsRequired" = ct."StampsRequired"
                FROM "CardTemplates" ct
                WHERE wp."CardTemplateId" = ct."Id"
                """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StampsRequired",
                table: "WalletPasses");
        }
    }
}
