using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ExperisEvaluacionAPI.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    FechaNacimiento = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Genero = table.Column<char>(type: "character(1)", maxLength: 1, nullable: false),
                    Roles = table.Column<string>(type: "text", nullable: false),
                    Bloquear = table.Column<bool>(type: "boolean", nullable: false),
                    MarcaTemporalCreacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UsuarioCreador = table.Column<string>(type: "text", nullable: false),
                    MarcaTemporalActualizacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UsuarioActualizador = table.Column<string>(type: "text", nullable: false),
                    MarcaTemporalEliminado = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UsuarioEliminador = table.Column<string>(type: "text", nullable: false),
                    EstadoEliminado = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuarios", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "usuarios");
        }
    }
}
