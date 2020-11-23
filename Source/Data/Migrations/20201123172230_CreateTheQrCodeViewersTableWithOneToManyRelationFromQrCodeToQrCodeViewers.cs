using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class CreateTheQrCodeViewersTableWithOneToManyRelationFromQrCodeToQrCodeViewers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "QrCodeViewer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    QrCodeId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QrCodeViewer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QrCodeViewer_QrCodes_QrCodeId",
                        column: x => x.QrCodeId,
                        principalTable: "QrCodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QrCodeViewer_QrCodeId",
                table: "QrCodeViewer",
                column: "QrCodeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QrCodeViewer");
        }
    }
}
