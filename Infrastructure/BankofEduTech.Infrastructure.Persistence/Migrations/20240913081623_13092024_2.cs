using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankofEduTech.Infrastructure.Persistence.Migrations
{
    public partial class _13092024_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ReferenceID",
                table: "CustomerAccountActivities",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReferenceID",
                table: "CustomerAccountActivities");
        }
    }
}
