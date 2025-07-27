using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace nastrafarmapi.Migrations
{
    /// <inheritdoc />
    public partial class GuidtoString : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Entrades_AspNetUsers_CreatedByUserId",
                table: "Entrades");

            migrationBuilder.DropForeignKey(
                name: "FK_Lots_AspNetUsers_CreatedByUserId",
                table: "Lots");

            migrationBuilder.RenameColumn(
                name: "CreatedByUserId",
                table: "Lots",
                newName: "UserGuid");

            migrationBuilder.RenameIndex(
                name: "IX_Lots_CreatedByUserId",
                table: "Lots",
                newName: "IX_Lots_UserGuid");

            migrationBuilder.RenameColumn(
                name: "CreatedByUserId",
                table: "Entrades",
                newName: "UserGuid");

            migrationBuilder.RenameIndex(
                name: "IX_Entrades_CreatedByUserId",
                table: "Entrades",
                newName: "IX_Entrades_UserGuid");

            migrationBuilder.AddForeignKey(
                name: "FK_Entrades_AspNetUsers_UserGuid",
                table: "Entrades",
                column: "UserGuid",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Lots_AspNetUsers_UserGuid",
                table: "Lots",
                column: "UserGuid",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Entrades_AspNetUsers_UserGuid",
                table: "Entrades");

            migrationBuilder.DropForeignKey(
                name: "FK_Lots_AspNetUsers_UserGuid",
                table: "Lots");

            migrationBuilder.RenameColumn(
                name: "UserGuid",
                table: "Lots",
                newName: "CreatedByUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Lots_UserGuid",
                table: "Lots",
                newName: "IX_Lots_CreatedByUserId");

            migrationBuilder.RenameColumn(
                name: "UserGuid",
                table: "Entrades",
                newName: "CreatedByUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Entrades_UserGuid",
                table: "Entrades",
                newName: "IX_Entrades_CreatedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Entrades_AspNetUsers_CreatedByUserId",
                table: "Entrades",
                column: "CreatedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Lots_AspNetUsers_CreatedByUserId",
                table: "Lots",
                column: "CreatedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
