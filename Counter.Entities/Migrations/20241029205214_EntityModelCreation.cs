using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Counter.Entities.Migrations
{
    /// <inheritdoc />
    public partial class EntityModelCreation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Counters",
                columns: table => new
                {
                    UUID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SeriNumarasi = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    OlcumZamani = table.Column<DateTime>(type: "datetime2(0)", nullable: false),
                    SonEndeks = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    Voltaj = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    Akim = table.Column<decimal>(type: "decimal(8,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Counters", x => x.UUID);
                });

            migrationBuilder.CreateTable(
                name: "Reports",
                columns: table => new
                {
                    UUID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TalepTarihi = table.Column<DateTime>(type: "datetime2(0)", nullable: false),
                    Durum = table.Column<byte>(type: "tinyint", nullable: false),
                    OlcumZamani = table.Column<DateTime>(type: "datetime2(0)", nullable: true),
                    SonEndeks = table.Column<decimal>(type: "decimal(8,2)", nullable: true),
                    Voltaj = table.Column<decimal>(type: "decimal(8,2)", nullable: true),
                    Akim = table.Column<decimal>(type: "decimal(8,2)", nullable: true)
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
