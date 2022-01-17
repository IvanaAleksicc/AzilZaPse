using Microsoft.EntityFrameworkCore.Migrations;

namespace WEB_PROJEKAT.Migrations
{
    public partial class V10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "pasID",
                table: "Udomitelji",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Udomitelji_pasID",
                table: "Udomitelji",
                column: "pasID");

            migrationBuilder.AddForeignKey(
                name: "FK_Udomitelji_Psi_pasID",
                table: "Udomitelji",
                column: "pasID",
                principalTable: "Psi",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Udomitelji_Psi_pasID",
                table: "Udomitelji");

            migrationBuilder.DropIndex(
                name: "IX_Udomitelji_pasID",
                table: "Udomitelji");

            migrationBuilder.DropColumn(
                name: "pasID",
                table: "Udomitelji");
        }
    }
}
