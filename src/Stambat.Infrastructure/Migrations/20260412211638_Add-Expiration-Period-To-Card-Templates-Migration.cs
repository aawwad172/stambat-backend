using System;

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Stambat.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddExpirationPeriodToCardTemplatesMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ExpiresAt",
                table: "WalletPasses",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CardType",
                table: "CardTemplates",
                type: "integer",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<int>(
                name: "ExpiryDurationInDays",
                table: "CardTemplates",
                type: "integer",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpiresAt",
                table: "WalletPasses");

            migrationBuilder.DropColumn(
                name: "CardType",
                table: "CardTemplates");

            migrationBuilder.DropColumn(
                name: "ExpiryDurationInDays",
                table: "CardTemplates");
        }
    }
}
