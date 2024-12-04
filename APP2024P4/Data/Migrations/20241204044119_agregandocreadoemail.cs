using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APP2024P4.Data.Migrations
{
    /// <inheritdoc />
    public partial class agregandocreadoemail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tareas_Colaboradores_ColaboradorId",
                table: "Tareas");

            migrationBuilder.DropIndex(
                name: "IX_Tareas_ColaboradorId",
                table: "Tareas");

            migrationBuilder.DropColumn(
                name: "ColaboradorId",
                table: "Tareas");

            migrationBuilder.AddColumn<string>(
                name: "CreadorEmail",
                table: "Tareas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Colaboradores_TareaId",
                table: "Colaboradores",
                column: "TareaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Colaboradores_Tareas_TareaId",
                table: "Colaboradores",
                column: "TareaId",
                principalTable: "Tareas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Colaboradores_Tareas_TareaId",
                table: "Colaboradores");

            migrationBuilder.DropIndex(
                name: "IX_Colaboradores_TareaId",
                table: "Colaboradores");

            migrationBuilder.DropColumn(
                name: "CreadorEmail",
                table: "Tareas");

            migrationBuilder.AddColumn<int>(
                name: "ColaboradorId",
                table: "Tareas",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tareas_ColaboradorId",
                table: "Tareas",
                column: "ColaboradorId",
                unique: true,
                filter: "[ColaboradorId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Tareas_Colaboradores_ColaboradorId",
                table: "Tareas",
                column: "ColaboradorId",
                principalTable: "Colaboradores",
                principalColumn: "Id");
        }
    }
}
