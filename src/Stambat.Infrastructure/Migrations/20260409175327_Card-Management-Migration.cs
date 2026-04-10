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
            migrationBuilder.DropIndex(
                name: "IX_Invitations_Email_TenantId_RoleId",
                table: "Invitations");

            migrationBuilder.CreateIndex(
                name: "IX_Invitations_Email_TenantId_RoleId",
                table: "Invitations",
                columns: new[] { "Email", "TenantId", "RoleId" },
                unique: true,
                filter: "\"IsUsed\" = false AND \"IsCancelled\" = false AND \"IsDeleted\" = false");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Invitations_Email_TenantId_RoleId",
                table: "Invitations");

            migrationBuilder.CreateIndex(
                name: "IX_Invitations_Email_TenantId_RoleId",
                table: "Invitations",
                columns: new[] { "Email", "TenantId", "RoleId" },
                unique: true,
                filter: "\"IsUsed\" = false AND \"IsCancelled\" = false");
        }
    }
}
