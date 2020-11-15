using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class RenameThePhoneColumnToPhoneNumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "Users",
                newName: "PhoneNumber");

            migrationBuilder.RenameIndex(
                name: "IX_Users_Phone",
                table: "Users",
                newName: "IX_Users_PhoneNumber");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "Users",
                newName: "Phone");

            migrationBuilder.RenameIndex(
                name: "IX_Users_PhoneNumber",
                table: "Users",
                newName: "IX_Users_Phone");
        }
    }
}
