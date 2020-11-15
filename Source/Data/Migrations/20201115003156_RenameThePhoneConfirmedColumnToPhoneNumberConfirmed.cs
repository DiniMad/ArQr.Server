using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class RenameThePhoneConfirmedColumnToPhoneNumberConfirmed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PhoneConfirmed",
                table: "Users",
                newName: "PhoneNumberConfirmed");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PhoneNumberConfirmed",
                table: "Users",
                newName: "PhoneConfirmed");
        }
    }
}
