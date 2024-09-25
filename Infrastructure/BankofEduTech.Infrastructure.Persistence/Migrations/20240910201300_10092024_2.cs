using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankofEduTech.Infrastructure.Persistence.Migrations
{
    public partial class _10092024_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerAccountActivities_CustomerAccounts_ReceiverID",
                table: "CustomerAccountActivities");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerAccountActivities_CustomerAccounts_SenderID",
                table: "CustomerAccountActivities");

            migrationBuilder.DropIndex(
                name: "IX_CustomerAccountActivities_ReceiverID",
                table: "CustomerAccountActivities");

            migrationBuilder.DropColumn(
                name: "ReceiverID",
                table: "CustomerAccountActivities");

            migrationBuilder.RenameColumn(
                name: "SenderID",
                table: "CustomerAccountActivities",
                newName: "CustomerAccountID");

            migrationBuilder.RenameIndex(
                name: "IX_CustomerAccountActivities_SenderID",
                table: "CustomerAccountActivities",
                newName: "IX_CustomerAccountActivities_CustomerAccountID");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerAccountActivities_CustomerAccounts_CustomerAccountID",
                table: "CustomerAccountActivities",
                column: "CustomerAccountID",
                principalTable: "CustomerAccounts",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerAccountActivities_CustomerAccounts_CustomerAccountID",
                table: "CustomerAccountActivities");

            migrationBuilder.RenameColumn(
                name: "CustomerAccountID",
                table: "CustomerAccountActivities",
                newName: "SenderID");

            migrationBuilder.RenameIndex(
                name: "IX_CustomerAccountActivities_CustomerAccountID",
                table: "CustomerAccountActivities",
                newName: "IX_CustomerAccountActivities_SenderID");

            migrationBuilder.AddColumn<int>(
                name: "ReceiverID",
                table: "CustomerAccountActivities",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAccountActivities_ReceiverID",
                table: "CustomerAccountActivities",
                column: "ReceiverID");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerAccountActivities_CustomerAccounts_ReceiverID",
                table: "CustomerAccountActivities",
                column: "ReceiverID",
                principalTable: "CustomerAccounts",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerAccountActivities_CustomerAccounts_SenderID",
                table: "CustomerAccountActivities",
                column: "SenderID",
                principalTable: "CustomerAccounts",
                principalColumn: "ID");
        }
    }
}
