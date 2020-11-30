using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class SeedTheAdminUserToUserTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Admin", "Email", "EmailConfirmed", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed" },
                values: new object[] { 1L, true, "admin@arqr.com", true, "AQAAAAEAACcQAAAAELOXD5Z4lKhjCEQExaoM+Z0q7BR/vISBHA1XNP6nxJI2MrCD/x6vVCqEHCQ+mOHITg==", "0000000000", true });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L);
        }
    }
}
