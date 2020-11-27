using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class CreateNaviagtionPropertyToSupportedMediaExtensionTableInMediaContentTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Extension",
                table: "MediaContents");

            migrationBuilder.AddColumn<byte>(
                name: "ExtensionId",
                table: "MediaContents",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.CreateIndex(
                name: "IX_MediaContents_ExtensionId",
                table: "MediaContents",
                column: "ExtensionId");

            migrationBuilder.AddForeignKey(
                name: "FK_MediaContents_SupportedMediaExtensions_ExtensionId",
                table: "MediaContents",
                column: "ExtensionId",
                principalTable: "SupportedMediaExtensions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MediaContents_SupportedMediaExtensions_ExtensionId",
                table: "MediaContents");

            migrationBuilder.DropIndex(
                name: "IX_MediaContents_ExtensionId",
                table: "MediaContents");

            migrationBuilder.DropColumn(
                name: "ExtensionId",
                table: "MediaContents");

            migrationBuilder.AddColumn<string>(
                name: "Extension",
                table: "MediaContents",
                type: "varchar(8)",
                unicode: false,
                maxLength: 8,
                nullable: false,
                defaultValue: "");
        }
    }
}
