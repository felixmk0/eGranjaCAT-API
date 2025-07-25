using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PorcXarxaAPI.Migrations
{
    /// <inheritdoc />
    public partial class EntradaTable2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Entrades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Categoria = table.Column<int>(type: "int", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NombreAnimals = table.Column<int>(type: "int", nullable: false),
                    PesTotal = table.Column<double>(type: "float", nullable: false),
                    PesIndividual = table.Column<double>(type: "float", nullable: false),
                    LotId = table.Column<int>(type: "int", nullable: false),
                    Origen = table.Column<int>(type: "int", nullable: false),
                    MarcaOficial = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CodiREGA = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumeroDocumentTrasllat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Observacions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FarmId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entrades", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Entrades_AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Entrades_Farms_FarmId",
                        column: x => x.FarmId,
                        principalTable: "Farms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Entrades_CreatedBy",
                table: "Entrades",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Entrades_FarmId",
                table: "Entrades",
                column: "FarmId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Entrades");
        }
    }
}
