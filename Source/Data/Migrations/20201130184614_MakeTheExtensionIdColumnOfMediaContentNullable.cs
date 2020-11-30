using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class MakeTheExtensionIdColumnOfMediaContentNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MediaContents_SupportedMediaExtensions_ExtensionId",
                table: "MediaContents");

            migrationBuilder.AlterColumn<byte>(
                name: "ExtensionId",
                table: "MediaContents",
                type: "tinyint",
                nullable: true,
                oldClrType: typeof(byte),
                oldType: "tinyint");

            migrationBuilder.AddForeignKey(
                name: "FK_MediaContents_SupportedMediaExtensions_ExtensionId",
                table: "MediaContents",
                column: "ExtensionId",
                principalTable: "SupportedMediaExtensions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MediaContents_SupportedMediaExtensions_ExtensionId",
                table: "MediaContents");

            migrationBuilder.AlterColumn<byte>(
                name: "ExtensionId",
                table: "MediaContents",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0,
                oldClrType: typeof(byte),
                oldType: "tinyint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MediaContents_SupportedMediaExtensions_ExtensionId",
                table: "MediaContents",
                column: "ExtensionId",
                principalTable: "SupportedMediaExtensions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
