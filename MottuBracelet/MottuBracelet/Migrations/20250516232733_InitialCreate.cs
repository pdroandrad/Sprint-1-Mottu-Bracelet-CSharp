using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MottuBracelet.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PATIO_NET",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Nome = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    CapacidadeMaxima = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    AdministradorResponsavel = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Endereco_Logradouro = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Endereco_Numero = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Endereco_Cep = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Endereco_Complemento = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    Endereco_Cidade = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Endereco_Pais = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PATIO_NET", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MOTO_NET",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Imei = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    Placa = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    PatioId = table.Column<int>(type: "NUMBER(10)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MOTO_NET", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MOTO_NET_PATIO_NET_PatioId",
                        column: x => x.PatioId,
                        principalTable: "PATIO_NET",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DISPOSITIVO_NET",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    StatusDispositivo = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    MotoId = table.Column<int>(type: "NUMBER(10)", nullable: true),
                    PatioId = table.Column<int>(type: "NUMBER(10)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DISPOSITIVO_NET", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DISPOSITIVO_NET_MOTO_NET_MotoId",
                        column: x => x.MotoId,
                        principalTable: "MOTO_NET",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DISPOSITIVO_NET_PATIO_NET_PatioId",
                        column: x => x.PatioId,
                        principalTable: "PATIO_NET",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "HISTORICOPATIO_NET",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    MotoId = table.Column<int>(type: "NUMBER(10)", nullable: true),
                    PatioId = table.Column<int>(type: "NUMBER(10)", nullable: true),
                    DataEntrada = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    DataSaida = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HISTORICOPATIO_NET", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HISTORICOPATIO_NET_MOTO_NET_MotoId",
                        column: x => x.MotoId,
                        principalTable: "MOTO_NET",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_HISTORICOPATIO_NET_PATIO_NET_PatioId",
                        column: x => x.PatioId,
                        principalTable: "PATIO_NET",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DISPOSITIVO_NET_MotoId",
                table: "DISPOSITIVO_NET",
                column: "MotoId",
                unique: true,
                filter: "\"MotoId\" IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_DISPOSITIVO_NET_PatioId",
                table: "DISPOSITIVO_NET",
                column: "PatioId");

            migrationBuilder.CreateIndex(
                name: "IX_HISTORICOPATIO_NET_MotoId",
                table: "HISTORICOPATIO_NET",
                column: "MotoId");

            migrationBuilder.CreateIndex(
                name: "IX_HISTORICOPATIO_NET_PatioId",
                table: "HISTORICOPATIO_NET",
                column: "PatioId");

            migrationBuilder.CreateIndex(
                name: "IX_MOTO_NET_PatioId",
                table: "MOTO_NET",
                column: "PatioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DISPOSITIVO_NET");

            migrationBuilder.DropTable(
                name: "HISTORICOPATIO_NET");

            migrationBuilder.DropTable(
                name: "MOTO_NET");

            migrationBuilder.DropTable(
                name: "PATIO_NET");
        }
    }
}
