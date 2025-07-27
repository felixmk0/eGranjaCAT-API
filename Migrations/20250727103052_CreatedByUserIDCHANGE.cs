using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace nastrafarmapi.Migrations
{
    /// <inheritdoc />
    public partial class CreatedByUserIDCHANGE : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Entrades_AspNetUsers_CreatedByUserId",
                table: "Entrades");

            migrationBuilder.DropForeignKey(
                name: "FK_Lots_AspNetUsers_CreatedBy",
                table: "Lots");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Entrades");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "Lots",
                newName: "CreatedByUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Lots_CreatedBy",
                table: "Lots",
                newName: "IX_Lots_CreatedByUserId");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedByUserId",
                table: "Entrades",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                newName: "CreatedBy");

            migrationBuilder.RenameIndex(
                name: "IX_Lots_CreatedByUserId",
                table: "Lots",
                newName: "IX_Lots_CreatedBy");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedByUserId",
                table: "Entrades",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Entrades",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Entrades_AspNetUsers_CreatedByUserId",
                table: "Entrades",
                column: "CreatedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Lots_AspNetUsers_CreatedBy",
                table: "Lots",
                column: "CreatedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
