using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class CreateOptionalOneToOneRelationFromQrCodeToMediaContent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "MediaContentId",
                table: "QrCodes",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_QrCodes_MediaContentId",
                table: "QrCodes",
                column: "MediaContentId");

            migrationBuilder.AddForeignKey(
                name: "FK_QrCodes_MediaContents_MediaContentId",
                table: "QrCodes",
                column: "MediaContentId",
                principalTable: "MediaContents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QrCodes_MediaContents_MediaContentId",
                table: "QrCodes");

            migrationBuilder.DropIndex(
                name: "IX_QrCodes_MediaContentId",
                table: "QrCodes");

            migrationBuilder.DropColumn(
                name: "MediaContentId",
                table: "QrCodes");
        }
    }
}
