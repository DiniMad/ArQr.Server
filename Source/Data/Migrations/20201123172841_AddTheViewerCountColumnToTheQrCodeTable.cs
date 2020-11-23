using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class AddTheViewerCountColumnToTheQrCodeTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ViewersCount",
                table: "QrCodes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ViewersCount",
                table: "QrCodes");
        }
    }
}
