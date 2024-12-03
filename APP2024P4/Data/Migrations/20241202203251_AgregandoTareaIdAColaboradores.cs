﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APP2024P4.Data.Migrations
{
    /// <inheritdoc />
    public partial class AgregandoTareaIdAColaboradores : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TareaId",
                table: "Colaboradores",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TareaId",
                table: "Colaboradores");
        }
    }
}
