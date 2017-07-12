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
                name: "Jahrgang",
                columns: table => new
                {
                    JahrgangID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jahrgang", x => x.JahrgangID);
                });

            migrationBuilder.CreateTable(
                name: "Person",
                columns: table => new
                {
                    PersonID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Benutzername = table.Column<string>(nullable: true),
                    Nachname = table.Column<string>(nullable: true),
                    Vorname = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.PersonID);
                });

            migrationBuilder.CreateTable(
                name: "Fach",
                columns: table => new
                {
                    FachID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    JahrgangID = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fach", x => x.FachID);
                    table.ForeignKey(
                        name: "FK_Fach_Jahrgang_JahrgangID",
                        column: x => x.JahrgangID,
                        principalTable: "Jahrgang",
                        principalColumn: "JahrgangID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Klasse",
                columns: table => new
                {
                    KlasseID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    JahrgangID = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Klasse", x => x.KlasseID);
                    table.ForeignKey(
                        name: "FK_Klasse_Jahrgang_JahrgangID",
                        column: x => x.JahrgangID,
                        principalTable: "Jahrgang",
                        principalColumn: "JahrgangID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Lehrer",
                columns: table => new
                {
                    LehrerID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PersonID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lehrer", x => x.LehrerID);
                    table.ForeignKey(
                        name: "FK_Lehrer_Person_PersonID",
                        column: x => x.PersonID,
                        principalTable: "Person",
                        principalColumn: "PersonID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Schueler",
                columns: table => new
                {
                    SchuelerID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PersonID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schueler", x => x.SchuelerID);
                    table.ForeignKey(
                        name: "FK_Schueler_Person_PersonID",
                        column: x => x.PersonID,
                        principalTable: "Person",
                        principalColumn: "PersonID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KlasseFach",
                columns: table => new
                {
                    KlasseID = table.Column<int>(nullable: false),
                    FachID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KlasseFach", x => new { x.KlasseID, x.FachID });
                    table.ForeignKey(
                        name: "FK_KlasseFach_Fach_FachID",
                        column: x => x.FachID,
                        principalTable: "Fach",
                        principalColumn: "FachID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KlasseFach_Klasse_KlasseID",
                        column: x => x.KlasseID,
                        principalTable: "Klasse",
                        principalColumn: "KlasseID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FachLehrer",
                columns: table => new
                {
                    FachID = table.Column<int>(nullable: false),
                    LehrerID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FachLehrer", x => new { x.FachID, x.LehrerID });
                    table.ForeignKey(
                        name: "FK_FachLehrer_Fach_FachID",
                        column: x => x.FachID,
                        principalTable: "Fach",
                        principalColumn: "FachID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FachLehrer_Lehrer_LehrerID",
                        column: x => x.LehrerID,
                        principalTable: "Lehrer",
                        principalColumn: "LehrerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KlasseLehrer",
                columns: table => new
                {
                    KlasseID = table.Column<int>(nullable: false),
                    LehrerID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KlasseLehrer", x => new { x.KlasseID, x.LehrerID });
                    table.ForeignKey(
                        name: "FK_KlasseLehrer_Klasse_KlasseID",
                        column: x => x.KlasseID,
                        principalTable: "Klasse",
                        principalColumn: "KlasseID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KlasseLehrer_Lehrer_LehrerID",
                        column: x => x.LehrerID,
                        principalTable: "Lehrer",
                        principalColumn: "LehrerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FachSchueler",
                columns: table => new
                {
                    FachID = table.Column<int>(nullable: false),
                    SchuelerID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FachSchueler", x => new { x.FachID, x.SchuelerID });
                    table.ForeignKey(
                        name: "FK_FachSchueler_Fach_FachID",
                        column: x => x.FachID,
                        principalTable: "Fach",
                        principalColumn: "FachID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FachSchueler_Schueler_SchuelerID",
                        column: x => x.SchuelerID,
                        principalTable: "Schueler",
                        principalColumn: "SchuelerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KlasseSchueler",
                columns: table => new
                {
                    KlasseID = table.Column<int>(nullable: false),
                    SchuelerID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KlasseSchueler", x => new { x.KlasseID, x.SchuelerID });
                    table.ForeignKey(
                        name: "FK_KlasseSchueler_Klasse_KlasseID",
                        column: x => x.KlasseID,
                        principalTable: "Klasse",
                        principalColumn: "KlasseID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KlasseSchueler_Schueler_SchuelerID",
                        column: x => x.SchuelerID,
                        principalTable: "Schueler",
                        principalColumn: "SchuelerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notenerhebung",
                columns: table => new
                {
                    NotenerhebungID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Bemerkung = table.Column<string>(nullable: true),
                    Datum = table.Column<DateTime>(nullable: false),
                    FachID = table.Column<int>(nullable: false),
                    Note = table.Column<int>(nullable: false),
                    SchuelerID = table.Column<int>(nullable: false),
                    Typ = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notenerhebung", x => x.NotenerhebungID);
                    table.ForeignKey(
                        name: "FK_Notenerhebung_Fach_FachID",
                        column: x => x.FachID,
                        principalTable: "Fach",
                        principalColumn: "FachID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Notenerhebung_Schueler_SchuelerID",
                        column: x => x.SchuelerID,
                        principalTable: "Schueler",
                        principalColumn: "SchuelerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Fach_JahrgangID",
                table: "Fach",
                column: "JahrgangID");

            migrationBuilder.CreateIndex(
                name: "IX_FachLehrer_LehrerID",
                table: "FachLehrer",
                column: "LehrerID");

            migrationBuilder.CreateIndex(
                name: "IX_FachSchueler_SchuelerID",
                table: "FachSchueler",
                column: "SchuelerID");

            migrationBuilder.CreateIndex(
                name: "IX_Klasse_JahrgangID",
                table: "Klasse",
                column: "JahrgangID");

            migrationBuilder.CreateIndex(
                name: "IX_KlasseFach_FachID",
                table: "KlasseFach",
                column: "FachID");

            migrationBuilder.CreateIndex(
                name: "IX_KlasseLehrer_LehrerID",
                table: "KlasseLehrer",
                column: "LehrerID");

            migrationBuilder.CreateIndex(
                name: "IX_KlasseSchueler_SchuelerID",
                table: "KlasseSchueler",
                column: "SchuelerID");

            migrationBuilder.CreateIndex(
                name: "IX_Lehrer_PersonID",
                table: "Lehrer",
                column: "PersonID");

            migrationBuilder.CreateIndex(
                name: "IX_Notenerhebung_FachID",
                table: "Notenerhebung",
                column: "FachID");

            migrationBuilder.CreateIndex(
                name: "IX_Notenerhebung_SchuelerID",
                table: "Notenerhebung",
                column: "SchuelerID");

            migrationBuilder.CreateIndex(
                name: "IX_Schueler_PersonID",
                table: "Schueler",
                column: "PersonID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FachLehrer");

            migrationBuilder.DropTable(
                name: "FachSchueler");

            migrationBuilder.DropTable(
                name: "KlasseFach");

            migrationBuilder.DropTable(
                name: "KlasseLehrer");

            migrationBuilder.DropTable(
                name: "KlasseSchueler");

            migrationBuilder.DropTable(
                name: "Notenerhebung");

            migrationBuilder.DropTable(
                name: "Lehrer");

            migrationBuilder.DropTable(
                name: "Klasse");

            migrationBuilder.DropTable(
                name: "Fach");

            migrationBuilder.DropTable(
                name: "Schueler");

            migrationBuilder.DropTable(
                name: "Jahrgang");

            migrationBuilder.DropTable(
                name: "Person");
        }
    }
}
