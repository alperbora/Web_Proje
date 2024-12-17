using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web_proje1.Migrations
{
    /// <inheritdoc />
    public partial class CreateCalisanlarTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Calisanlar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Soyad = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calisanlar", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UzmanlikAlanlari",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UzmanlikAlanlari", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Randevular",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RandevuZamani = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Islem = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sure = table.Column<int>(type: "int", nullable: false),
                    Ucret = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OnayliMi = table.Column<bool>(type: "bit", nullable: false),
                    CalisanId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Randevular", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Randevular_Calisanlar_CalisanId",
                        column: x => x.CalisanId,
                        principalTable: "Calisanlar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UygunlukSaatleri",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CalisanId = table.Column<int>(type: "int", nullable: false),
                    BaslangicSaati = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BitisSaati = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UygunlukSaatleri", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UygunlukSaatleri_Calisanlar_CalisanId",
                        column: x => x.CalisanId,
                        principalTable: "Calisanlar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CalisanUzmanlikAlani",
                columns: table => new
                {
                    CalisanlarId = table.Column<int>(type: "int", nullable: false),
                    UzmanlikAlanlariId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalisanUzmanlikAlani", x => new { x.CalisanlarId, x.UzmanlikAlanlariId });
                    table.ForeignKey(
                        name: "FK_CalisanUzmanlikAlani_Calisanlar_CalisanlarId",
                        column: x => x.CalisanlarId,
                        principalTable: "Calisanlar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CalisanUzmanlikAlani_UzmanlikAlanlari_UzmanlikAlanlariId",
                        column: x => x.UzmanlikAlanlariId,
                        principalTable: "UzmanlikAlanlari",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CalisanUzmanlikAlani_UzmanlikAlanlariId",
                table: "CalisanUzmanlikAlani",
                column: "UzmanlikAlanlariId");

            migrationBuilder.CreateIndex(
                name: "IX_Randevular_CalisanId",
                table: "Randevular",
                column: "CalisanId");

            migrationBuilder.CreateIndex(
                name: "IX_UygunlukSaatleri_CalisanId",
                table: "UygunlukSaatleri",
                column: "CalisanId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CalisanUzmanlikAlani");

            migrationBuilder.DropTable(
                name: "Randevular");

            migrationBuilder.DropTable(
                name: "UygunlukSaatleri");

            migrationBuilder.DropTable(
                name: "UzmanlikAlanlari");

            migrationBuilder.DropTable(
                name: "Calisanlar");
        }
    }
}
