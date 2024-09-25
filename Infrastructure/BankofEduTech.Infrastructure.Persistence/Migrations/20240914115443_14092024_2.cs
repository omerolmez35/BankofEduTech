using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankofEduTech.Infrastructure.Persistence.Migrations
{
    public partial class _14092024_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CustomerCreditInstallements",
                columns: table => new
                {
                    InstallementsID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumberOfInstallements = table.Column<int>(type: "int", nullable: false),
                    AmountOfInstallements = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AmountOfInterest = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AmountOfPrincipal = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RestOfPrincipal = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpireDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsPaid = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerCreditInstallements", x => x.InstallementsID);
                });

            migrationBuilder.CreateTable(
                name: "CustomerCredits",
                columns: table => new
                {
                    CreditID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumberofInstallement = table.Column<int>(type: "int", nullable: false),
                    LastInstallementDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AmountOfInstallement = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CustomerAccountID = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerCredits", x => x.CreditID);
                    table.ForeignKey(
                        name: "FK_CustomerCredits_CustomerAccounts_CustomerAccountID",
                        column: x => x.CustomerAccountID,
                        principalTable: "CustomerAccounts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerCredits_CustomerAccountID",
                table: "CustomerCredits",
                column: "CustomerAccountID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerCreditInstallements");

            migrationBuilder.DropTable(
                name: "CustomerCredits");
        }
    }
}
