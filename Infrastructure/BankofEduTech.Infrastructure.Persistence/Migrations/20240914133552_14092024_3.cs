using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankofEduTech.Infrastructure.Persistence.Migrations
{
    public partial class _14092024_3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CustomerCreditID",
                table: "CustomerCreditInstallements",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CustomerCreditInstallements_CustomerCreditID",
                table: "CustomerCreditInstallements",
                column: "CustomerCreditID");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerCreditInstallements_CustomerCredits_CustomerCreditID",
                table: "CustomerCreditInstallements",
                column: "CustomerCreditID",
                principalTable: "CustomerCredits",
                principalColumn: "CreditID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerCreditInstallements_CustomerCredits_CustomerCreditID",
                table: "CustomerCreditInstallements");

            migrationBuilder.DropIndex(
                name: "IX_CustomerCreditInstallements_CustomerCreditID",
                table: "CustomerCreditInstallements");

            migrationBuilder.DropColumn(
                name: "CustomerCreditID",
                table: "CustomerCreditInstallements");
        }
    }
}
