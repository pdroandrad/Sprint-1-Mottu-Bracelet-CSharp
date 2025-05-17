using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MottuBracelet.Migrations
{
    /// <inheritdoc />
    public partial class MakeDispositivoNullableInMoto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DispositivoId",
                table: "MOTO_NET",
                type: "NUMBER(10)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DispositivoId",
                table: "MOTO_NET");
        }
    }
}
