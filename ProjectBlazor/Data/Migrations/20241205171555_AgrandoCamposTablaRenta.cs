using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectBlazor.Data.Migrations
{
    /// <inheritdoc />
    public partial class AgrandoCamposTablaRenta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DiasRentado",
                table: "Rentas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Precio",
                table: "Rentas",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiasRentado",
                table: "Rentas");

            migrationBuilder.DropColumn(
                name: "Precio",
                table: "Rentas");
        }
    }
}
