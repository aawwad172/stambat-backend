using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Stambat.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenameLastStampedAtAndRemoveRedemptionCount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RedemptionCount",
                table: "WalletPasses");

            migrationBuilder.RenameColumn(
                name: "LastStampedAt",
                table: "WalletPasses",
                newName: "LastProgressAt");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastProgressAt",
                table: "WalletPasses",
                newName: "LastStampedAt");

            migrationBuilder.AddColumn<int>(
                name: "RedemptionCount",
                table: "WalletPasses",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
