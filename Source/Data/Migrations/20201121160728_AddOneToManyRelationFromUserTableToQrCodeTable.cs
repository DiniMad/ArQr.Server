using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class AddOneToManyRelationFromUserTableToQrCodeTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "OwnerId",
                table: "QrCodes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_QrCodes_OwnerId",
                table: "QrCodes",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_QrCodes_Users_OwnerId",
                table: "QrCodes",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QrCodes_Users_OwnerId",
                table: "QrCodes");

            migrationBuilder.DropIndex(
                name: "IX_QrCodes_OwnerId",
                table: "QrCodes");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "QrCodes");
        }
    }
}
