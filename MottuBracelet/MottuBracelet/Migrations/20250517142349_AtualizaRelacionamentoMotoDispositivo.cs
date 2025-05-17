using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MottuBracelet.Migrations
{
    /// <inheritdoc />
    public partial class AtualizaRelacionamentoMotoDispositivo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DISPOSITIVO_NET_MOTO_NET_MotoId",
                table: "DISPOSITIVO_NET");

            migrationBuilder.AddForeignKey(
                name: "FK_DISPOSITIVO_NET_MOTO_NET_MotoId",
                table: "DISPOSITIVO_NET",
                column: "MotoId",
                principalTable: "MOTO_NET",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DISPOSITIVO_NET_MOTO_NET_MotoId",
                table: "DISPOSITIVO_NET");

            migrationBuilder.AddForeignKey(
                name: "FK_DISPOSITIVO_NET_MOTO_NET_MotoId",
                table: "DISPOSITIVO_NET",
                column: "MotoId",
                principalTable: "MOTO_NET",
                principalColumn: "Id");
        }
    }
}
