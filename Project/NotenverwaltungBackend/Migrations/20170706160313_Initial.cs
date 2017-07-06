using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace NotenverwaltungBackend.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SchuelerNoten",
                columns: table => new
                {
                    Benutzername = table.Column<string>(nullable: false),
                    Halbjahrenszeugnis = table.Column<string>(nullable: true),
                    Jahreszeugnis = table.Column<string>(nullable: true),
                    Klasse = table.Column<string>(nullable: true),
                    Nachname = table.Column<string>(nullable: true),
                    Vorname = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchuelerNoten", x => x.Benutzername);
                });

            migrationBuilder.CreateTable(
                name: "Fach",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    SchuelerNotenBenutzername = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fach", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Fach_SchuelerNoten_SchuelerNotenBenutzername",
                        column: x => x.SchuelerNotenBenutzername,
                        principalTable: "SchuelerNoten",
                        principalColumn: "Benutzername",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Leistungserhebung",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Datum = table.Column<DateTime>(nullable: false),
                    FachID = table.Column<int>(nullable: true),
                    Note = table.Column<double>(nullable: false),
                    Typ = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Leistungserhebung", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Leistungserhebung_Fach_FachID",
                        column: x => x.FachID,
                        principalTable: "Fach",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Fach_SchuelerNotenBenutzername",
                table: "Fach",
                column: "SchuelerNotenBenutzername");

            migrationBuilder.CreateIndex(
                name: "IX_Leistungserhebung_FachID",
                table: "Leistungserhebung",
                column: "FachID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Leistungserhebung");

            migrationBuilder.DropTable(
                name: "Fach");

            migrationBuilder.DropTable(
                name: "SchuelerNoten");
        }
    }
}
