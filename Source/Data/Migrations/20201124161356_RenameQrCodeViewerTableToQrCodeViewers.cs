using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class RenameQrCodeViewerTableToQrCodeViewers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QrCodeViewer_QrCodes_QrCodeId",
                table: "QrCodeViewer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QrCodeViewer",
                table: "QrCodeViewer");

            migrationBuilder.RenameTable(
                name: "QrCodeViewer",
                newName: "QrCodeViewers");

            migrationBuilder.RenameIndex(
                name: "IX_QrCodeViewer_QrCodeId",
                table: "QrCodeViewers",
                newName: "IX_QrCodeViewers_QrCodeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QrCodeViewers",
                table: "QrCodeViewers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_QrCodeViewers_QrCodes_QrCodeId",
                table: "QrCodeViewers",
                column: "QrCodeId",
                principalTable: "QrCodes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QrCodeViewers_QrCodes_QrCodeId",
                table: "QrCodeViewers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QrCodeViewers",
                table: "QrCodeViewers");

            migrationBuilder.RenameTable(
                name: "QrCodeViewers",
                newName: "QrCodeViewer");

            migrationBuilder.RenameIndex(
                name: "IX_QrCodeViewers_QrCodeId",
                table: "QrCodeViewer",
                newName: "IX_QrCodeViewer_QrCodeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QrCodeViewer",
                table: "QrCodeViewer",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_QrCodeViewer_QrCodes_QrCodeId",
                table: "QrCodeViewer",
                column: "QrCodeId",
                principalTable: "QrCodes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
