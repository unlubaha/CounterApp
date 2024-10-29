using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Counter.ReportService.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Counters",
                columns: table => new
                {
                    UUID = table.Column<Guid>(type: "uuid", nullable: false),
                    SeriNumarasi = table.Column<string>(type: "text", nullable: true),
                    OlcumZamani = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    SonEndeks = table.Column<double>(type: "double precision", nullable: false),
                    Voltaj = table.Column<double>(type: "double precision", nullable: false),
                    Akim = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Counters", x => x.UUID);
                });

            migrationBuilder.CreateTable(
                name: "Reports",
                columns: table => new
                {
                    UUID = table.Column<Guid>(type: "uuid", nullable: false),
                    TalepTarihi = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Durum = table.Column<byte>(type: "smallint", nullable: false),
                    Icerik_OlcumZamani = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Icerik_SonEndeks = table.Column<int>(type: "integer", nullable: true),
                    Icerik_Voltaj = table.Column<double>(type: "double precision", nullable: true),
                    Icerik_Akim = table.Column<double>(type: "double precision", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reports", x => x.UUID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Counters");

            migrationBuilder.DropTable(
                name: "Reports");
        }
    }
}
