using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectBlazor.Data.Migrations
{
    /// <inheritdoc />
    public partial class AgregandoTablaRentas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RentaId",
                table: "Vehiculos",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RentaId",
                table: "Clientes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Rentas",
                columns: table => new
                {
                    RentaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaRenta = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaEntrega = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TotalPagado = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    VehiculoId = table.Column<int>(type: "int", nullable: true),
                    ClienteId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rentas", x => x.RentaId);
                    table.ForeignKey(
                        name: "FK_Rentas_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "ClienteId");
                    table.ForeignKey(
                        name: "FK_Rentas_Vehiculos_VehiculoId",
                        column: x => x.VehiculoId,
                        principalTable: "Vehiculos",
                        principalColumn: "VehiculoId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vehiculos_RentaId",
                table: "Vehiculos",
                column: "RentaId");

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_RentaId",
                table: "Clientes",
                column: "RentaId");

            migrationBuilder.CreateIndex(
                name: "IX_Rentas_ClienteId",
                table: "Rentas",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Rentas_VehiculoId",
                table: "Rentas",
                column: "VehiculoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clientes_Rentas_RentaId",
                table: "Clientes",
                column: "RentaId",
                principalTable: "Rentas",
                principalColumn: "RentaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehiculos_Rentas_RentaId",
                table: "Vehiculos",
                column: "RentaId",
                principalTable: "Rentas",
                principalColumn: "RentaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clientes_Rentas_RentaId",
                table: "Clientes");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehiculos_Rentas_RentaId",
                table: "Vehiculos");

            migrationBuilder.DropTable(
                name: "Rentas");

            migrationBuilder.DropIndex(
                name: "IX_Vehiculos_RentaId",
                table: "Vehiculos");

            migrationBuilder.DropIndex(
                name: "IX_Clientes_RentaId",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "RentaId",
                table: "Vehiculos");

            migrationBuilder.DropColumn(
                name: "RentaId",
                table: "Clientes");
        }
    }
}
