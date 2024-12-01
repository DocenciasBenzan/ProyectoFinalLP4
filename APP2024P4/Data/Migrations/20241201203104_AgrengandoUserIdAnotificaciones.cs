using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APP2024P4.Data.Migrations
{
    /// <inheritdoc />
    public partial class AgrengandoUserIdAnotificaciones : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Notificaciones",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Notificaciones_UserId",
                table: "Notificaciones",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notificaciones_AspNetUsers_UserId",
                table: "Notificaciones",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notificaciones_AspNetUsers_UserId",
                table: "Notificaciones");

            migrationBuilder.DropIndex(
                name: "IX_Notificaciones_UserId",
                table: "Notificaciones");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Notificaciones");
        }
    }
}
