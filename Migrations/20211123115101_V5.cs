using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WEB_PROJEKAT.Migrations
{
    public partial class V5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Azil",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Grad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ulica = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostanskiBroj = table.Column<int>(type: "int", nullable: false),
                    BrojPasa = table.Column<int>(type: "int", nullable: false),
                    Kapacitet = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Azil", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Rase",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rase", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Pas",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Starost = table.Column<int>(type: "int", nullable: false),
                    Tezina = table.Column<int>(type: "int", nullable: false),
                    rasaID = table.Column<int>(type: "int", nullable: true),
                    Pol = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    datum_rodjenja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Velicina = table.Column<int>(type: "int", nullable: false),
                    azilID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pas", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Pas_Azil_azilID",
                        column: x => x.azilID,
                        principalTable: "Azil",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Pas_Rase_rasaID",
                        column: x => x.rasaID,
                        principalTable: "Rase",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Udomitelji",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Prezime = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Grad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ulica = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostanskiBroj = table.Column<int>(type: "int", nullable: false),
                    pasID = table.Column<int>(type: "int", nullable: true),
                    AzilID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Udomitelji", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Udomitelji_Azil_AzilID",
                        column: x => x.AzilID,
                        principalTable: "Azil",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Udomitelji_Pas_pasID",
                        column: x => x.pasID,
                        principalTable: "Pas",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pas_azilID",
                table: "Pas",
                column: "azilID");

            migrationBuilder.CreateIndex(
                name: "IX_Pas_rasaID",
                table: "Pas",
                column: "rasaID");

            migrationBuilder.CreateIndex(
                name: "IX_Udomitelji_AzilID",
                table: "Udomitelji",
                column: "AzilID");

            migrationBuilder.CreateIndex(
                name: "IX_Udomitelji_pasID",
                table: "Udomitelji",
                column: "pasID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Udomitelji");

            migrationBuilder.DropTable(
                name: "Pas");

            migrationBuilder.DropTable(
                name: "Azil");

            migrationBuilder.DropTable(
                name: "Rase");
        }
    }
}
