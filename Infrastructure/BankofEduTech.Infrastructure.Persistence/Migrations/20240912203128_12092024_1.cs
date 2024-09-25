using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankofEduTech.Infrastructure.Persistence.Migrations
{
    public partial class _12092024_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IdentityNumber",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdentityNumber",
                table: "AspNetUsers");
        }
    }
}
