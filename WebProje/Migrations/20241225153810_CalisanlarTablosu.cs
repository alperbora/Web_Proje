using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebProje.Migrations
{
    /// <inheritdoc />
    public partial class CalisanlarTablosu : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Calisanlar_Salonlar_SalonId",
                table: "Calisanlar");

            migrationBuilder.DropIndex(
                name: "IX_Calisanlar_SalonId",
                table: "Calisanlar");

            migrationBuilder.DropColumn(
                name: "CalismaSaatleri",
                table: "Calisanlar");

            migrationBuilder.DropColumn(
                name: "SalonId",
                table: "Calisanlar");

            migrationBuilder.RenameColumn(
                name: "Soyad",
                table: "Calisanlar",
                newName: "UygunSaatler");

            migrationBuilder.RenameColumn(
                name: "Ad",
                table: "Calisanlar",
                newName: "Beceriler");

            migrationBuilder.AddColumn<string>(
                name: "AdSoyad",
                table: "Calisanlar",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdSoyad",
                table: "Calisanlar");

            migrationBuilder.RenameColumn(
                name: "UygunSaatler",
                table: "Calisanlar",
                newName: "Soyad");

            migrationBuilder.RenameColumn(
                name: "Beceriler",
                table: "Calisanlar",
                newName: "Ad");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "CalismaSaatleri",
                table: "Calisanlar",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<int>(
                name: "SalonId",
                table: "Calisanlar",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Calisanlar_SalonId",
                table: "Calisanlar",
                column: "SalonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Calisanlar_Salonlar_SalonId",
                table: "Calisanlar",
                column: "SalonId",
                principalTable: "Salonlar",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
