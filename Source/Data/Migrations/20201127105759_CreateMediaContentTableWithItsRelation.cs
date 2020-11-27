using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class CreateMediaContentTableWithItsRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MediaContents",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Extension = table.Column<string>(type: "varchar(8)", unicode: false, maxLength: 8, nullable: false),
                    Verified = table.Column<bool>(type: "bit", nullable: false),
                    TotalSizeInMb = table.Column<byte>(type: "tinyint", nullable: false),
                    CreationDate = table.Column<int>(type: "int", nullable: false),
                    ExpireDate = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediaContents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MediaContents_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MediaContents_UserId",
                table: "MediaContents",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MediaContents");
        }
    }
}
