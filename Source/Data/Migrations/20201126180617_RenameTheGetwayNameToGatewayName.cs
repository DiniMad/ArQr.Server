using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class RenameTheGetwayNameToGatewayName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "GetwayName",
                table: "Purchases",
                newName: "GatewayName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "GatewayName",
                table: "Purchases",
                newName: "GetwayName");
        }
    }
}
