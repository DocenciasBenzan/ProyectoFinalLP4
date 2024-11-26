using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APP2024P4.Migrations
{
    /// <inheritdoc />
    public partial class CambiosEntidades : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservas_Vehiculos_VehiculoId",
                table: "Reservas");

            migrationBuilder.DropIndex(
                name: "IX_Reservas_VehiculoId",
                table: "Reservas");

            migrationBuilder.AddColumn<int>(
                name: "ClientId",
                table: "Vehiculos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "NotasAdicionales",
                table: "Reservas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vehiculos_ClientId",
                table: "Vehiculos",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehiculos_Clientes_ClientId",
                table: "Vehiculos",
                column: "ClientId",
                principalTable: "Clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehiculos_Clientes_ClientId",
                table: "Vehiculos");

            migrationBuilder.DropIndex(
                name: "IX_Vehiculos_ClientId",
                table: "Vehiculos");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "Vehiculos");

            migrationBuilder.DropColumn(
                name: "NotasAdicionales",
                table: "Reservas");

            migrationBuilder.CreateIndex(
                name: "IX_Reservas_VehiculoId",
                table: "Reservas",
                column: "VehiculoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservas_Vehiculos_VehiculoId",
                table: "Reservas",
                column: "VehiculoId",
                principalTable: "Vehiculos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
