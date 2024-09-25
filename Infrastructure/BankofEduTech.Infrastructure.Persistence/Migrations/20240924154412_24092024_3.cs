using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankofEduTech.Infrastructure.Persistence.Migrations
{
    public partial class _24092024_3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerCredits_CustomerAccounts_CustomerAccountID",
                table: "CustomerCredits");

            migrationBuilder.AlterColumn<int>(
                name: "CustomerAccountID",
                table: "CustomerCredits",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<decimal>(
                name: "InterestRate",
                table: "CustomerCredits",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "PaymentAccountID",
                table: "CustomerCredits",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerCredits_CustomerAccounts_CustomerAccountID",
                table: "CustomerCredits",
                column: "CustomerAccountID",
                principalTable: "CustomerAccounts",
                principalColumn: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerCredits_CustomerAccounts_CustomerAccountID",
                table: "CustomerCredits");

            migrationBuilder.DropColumn(
                name: "InterestRate",
                table: "CustomerCredits");

            migrationBuilder.DropColumn(
                name: "PaymentAccountID",
                table: "CustomerCredits");

            migrationBuilder.AlterColumn<int>(
                name: "CustomerAccountID",
                table: "CustomerCredits",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerCredits_CustomerAccounts_CustomerAccountID",
                table: "CustomerCredits",
                column: "CustomerAccountID",
                principalTable: "CustomerAccounts",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
