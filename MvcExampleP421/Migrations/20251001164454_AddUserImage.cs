using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MvcExampleP421.Migrations
{
    /// <inheritdoc />
    public partial class AddUserImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ImageFileId",
                table: "AspNetUsers",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ImageFileId",
                table: "AspNetUsers",
                column: "ImageFileId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_ImageFiles_ImageFileId",
                table: "AspNetUsers",
                column: "ImageFileId",
                principalTable: "ImageFiles",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_ImageFiles_ImageFileId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ImageFileId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ImageFileId",
                table: "AspNetUsers");
        }
    }
}
