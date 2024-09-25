using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankofEduTech.Infrastructure.Persistence.Migrations
{
    public partial class _24092024_4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserID",
                table: "CustomerCredits",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_CustomerCredits_UserID",
                table: "CustomerCredits",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerCredits_AspNetUsers_UserID",
                table: "CustomerCredits",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerCredits_AspNetUsers_UserID",
                table: "CustomerCredits");

            migrationBuilder.DropIndex(
                name: "IX_CustomerCredits_UserID",
                table: "CustomerCredits");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "CustomerCredits");
        }
    }
}
