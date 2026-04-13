using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Stambat.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddRedemptionTypeAndRenameColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // 1. Rename columns (preserves data)
            migrationBuilder.RenameColumn(
                name: "CurrentStamps",
                table: "WalletPasses",
                newName: "CurrentBalance");

            migrationBuilder.RenameColumn(
                name: "StampsRequired",
                table: "WalletPasses",
                newName: "RequiredBalance");

            migrationBuilder.RenameColumn(
                name: "StampsAdded",
                table: "StampTransactions",
                newName: "AmountAdded");

            migrationBuilder.RenameColumn(
                name: "StampsRequired",
                table: "CardTemplates",
                newName: "RequiredBalance");

            // 2. Change column types from int to numeric (decimal)
            migrationBuilder.AlterColumn<decimal>(
                name: "CurrentBalance",
                table: "WalletPasses",
                type: "numeric",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(int),
                oldDefaultValue: 0);

            migrationBuilder.AlterColumn<decimal>(
                name: "RequiredBalance",
                table: "WalletPasses",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<decimal>(
                name: "AmountAdded",
                table: "StampTransactions",
                type: "numeric",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(int),
                oldDefaultValue: 1);

            migrationBuilder.AlterColumn<decimal>(
                name: "RequiredBalance",
                table: "CardTemplates",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(int));

            // 3. Add new columns
            migrationBuilder.AddColumn<int>(
                name: "RedemptionType",
                table: "WalletPasses",
                type: "integer",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<int>(
                name: "RedemptionType",
                table: "CardTemplates",
                type: "integer",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<decimal>(
                name: "PointsPerCurrencyUnit",
                table: "CardTemplates",
                type: "numeric",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Drop new columns
            migrationBuilder.DropColumn(name: "RedemptionType", table: "WalletPasses");
            migrationBuilder.DropColumn(name: "RedemptionType", table: "CardTemplates");
            migrationBuilder.DropColumn(name: "PointsPerCurrencyUnit", table: "CardTemplates");

            // Revert column types from numeric back to int
            migrationBuilder.AlterColumn<int>(
                name: "CurrentBalance",
                table: "WalletPasses",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(decimal),
                oldDefaultValue: 0m);

            migrationBuilder.AlterColumn<int>(
                name: "RequiredBalance",
                table: "WalletPasses",
                type: "integer",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<int>(
                name: "AmountAdded",
                table: "StampTransactions",
                type: "integer",
                nullable: false,
                defaultValue: 1,
                oldClrType: typeof(decimal),
                oldDefaultValue: 0m);

            migrationBuilder.AlterColumn<int>(
                name: "RequiredBalance",
                table: "CardTemplates",
                type: "integer",
                nullable: false,
                oldClrType: typeof(decimal));

            // Rename columns back
            migrationBuilder.RenameColumn(name: "CurrentBalance", table: "WalletPasses", newName: "CurrentStamps");
            migrationBuilder.RenameColumn(name: "RequiredBalance", table: "WalletPasses", newName: "StampsRequired");
            migrationBuilder.RenameColumn(name: "AmountAdded", table: "StampTransactions", newName: "StampsAdded");
            migrationBuilder.RenameColumn(name: "RequiredBalance", table: "CardTemplates", newName: "StampsRequired");
        }
    }
}
