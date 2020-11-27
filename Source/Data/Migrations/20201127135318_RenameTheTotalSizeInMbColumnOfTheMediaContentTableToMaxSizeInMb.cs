using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class RenameTheTotalSizeInMbColumnOfTheMediaContentTableToMaxSizeInMb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TotalSizeInMb",
                table: "MediaContents",
                newName: "MaxSizeInMb");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MaxSizeInMb",
                table: "MediaContents",
                newName: "TotalSizeInMb");
        }
    }
}
