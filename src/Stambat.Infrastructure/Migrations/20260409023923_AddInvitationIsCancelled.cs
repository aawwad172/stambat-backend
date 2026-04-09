using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Stambat.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddInvitationIsCancelled : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Invitations_Email_TenantId_RoleId",
                table: "Invitations");

            migrationBuilder.AddColumn<bool>(
                name: "IsCancelled",
                table: "Invitations",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Invitations_Email_TenantId_RoleId",
                table: "Invitations",
                columns: new[] { "Email", "TenantId", "RoleId" },
                unique: true,
                filter: "\"IsUsed\" = false AND \"IsCancelled\" = false");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Invitations_Email_TenantId_RoleId",
                table: "Invitations");

            migrationBuilder.DropColumn(
                name: "IsCancelled",
                table: "Invitations");

            migrationBuilder.CreateIndex(
                name: "IX_Invitations_Email_TenantId_RoleId",
                table: "Invitations",
                columns: new[] { "Email", "TenantId", "RoleId" },
                unique: true,
                filter: "\"IsUsed\" = false");
        }
    }
}
