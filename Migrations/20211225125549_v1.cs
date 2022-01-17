using Microsoft.EntityFrameworkCore.Migrations;

namespace WEB_PROJEKAT.Migrations
{
    public partial class v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pas_Azil_azilID",
                table: "Pas");

            migrationBuilder.DropForeignKey(
                name: "FK_Pas_Rase_rasaID",
                table: "Pas");

            migrationBuilder.DropForeignKey(
                name: "FK_Udomitelji_Azil_AzilID",
                table: "Udomitelji");

            migrationBuilder.DropForeignKey(
                name: "FK_Udomitelji_Pas_pasID",
                table: "Udomitelji");

            migrationBuilder.DropIndex(
                name: "IX_Udomitelji_pasID",
                table: "Udomitelji");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pas",
                table: "Pas");

            migrationBuilder.DropColumn(
                name: "pasID",
                table: "Udomitelji");

            migrationBuilder.RenameTable(
                name: "Pas",
                newName: "Psi");

            migrationBuilder.RenameColumn(
                name: "AzilID",
                table: "Udomitelji",
                newName: "azilID");

            migrationBuilder.RenameIndex(
                name: "IX_Udomitelji_AzilID",
                table: "Udomitelji",
                newName: "IX_Udomitelji_azilID");

            migrationBuilder.RenameIndex(
                name: "IX_Pas_rasaID",
                table: "Psi",
                newName: "IX_Psi_rasaID");

            migrationBuilder.RenameIndex(
                name: "IX_Pas_azilID",
                table: "Psi",
                newName: "IX_Psi_azilID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Psi",
                table: "Psi",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Psi_Azil_azilID",
                table: "Psi",
                column: "azilID",
                principalTable: "Azil",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Psi_Rase_rasaID",
                table: "Psi",
                column: "rasaID",
                principalTable: "Rase",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Udomitelji_Azil_azilID",
                table: "Udomitelji",
                column: "azilID",
                principalTable: "Azil",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Psi_Azil_azilID",
                table: "Psi");

            migrationBuilder.DropForeignKey(
                name: "FK_Psi_Rase_rasaID",
                table: "Psi");

            migrationBuilder.DropForeignKey(
                name: "FK_Udomitelji_Azil_azilID",
                table: "Udomitelji");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Psi",
                table: "Psi");

            migrationBuilder.RenameTable(
                name: "Psi",
                newName: "Pas");

            migrationBuilder.RenameColumn(
                name: "azilID",
                table: "Udomitelji",
                newName: "AzilID");

            migrationBuilder.RenameIndex(
                name: "IX_Udomitelji_azilID",
                table: "Udomitelji",
                newName: "IX_Udomitelji_AzilID");

            migrationBuilder.RenameIndex(
                name: "IX_Psi_rasaID",
                table: "Pas",
                newName: "IX_Pas_rasaID");

            migrationBuilder.RenameIndex(
                name: "IX_Psi_azilID",
                table: "Pas",
                newName: "IX_Pas_azilID");

            migrationBuilder.AddColumn<int>(
                name: "pasID",
                table: "Udomitelji",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pas",
                table: "Pas",
                column: "ID");

            migrationBuilder.CreateIndex(
                name: "IX_Udomitelji_pasID",
                table: "Udomitelji",
                column: "pasID");

            migrationBuilder.AddForeignKey(
                name: "FK_Pas_Azil_azilID",
                table: "Pas",
                column: "azilID",
                principalTable: "Azil",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Pas_Rase_rasaID",
                table: "Pas",
                column: "rasaID",
                principalTable: "Rase",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Udomitelji_Azil_AzilID",
                table: "Udomitelji",
                column: "AzilID",
                principalTable: "Azil",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Udomitelji_Pas_pasID",
                table: "Udomitelji",
                column: "pasID",
                principalTable: "Pas",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
