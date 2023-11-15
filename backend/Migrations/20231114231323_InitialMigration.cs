using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "empresa_cad",
                columns: table => new
                {
                    EmpresaId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Document = table.Column<string>(type: "text", nullable: false),
                    Uf = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_empresa_cad", x => x.EmpresaId);
                });

            migrationBuilder.CreateTable(
                name: "fornecedors_cad",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Document = table.Column<string>(type: "text", nullable: false),
                    DataCad = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Rg = table.Column<string>(type: "text", nullable: true),
                    DataNasc = table.Column<DateOnly>(type: "date", nullable: true),
                    Uf = table.Column<string>(type: "text", nullable: false),
                    TipoFornecedor = table.Column<string>(type: "text", nullable: false),
                    EmpresaVinculada = table.Column<int>(type: "integer", nullable: false),
                    Telefone_1 = table.Column<string>(type: "text", nullable: false),
                    Telefone_2 = table.Column<string>(type: "text", nullable: false),
                    Celular = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fornecedors_cad", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "empresa_cad");

            migrationBuilder.DropTable(
                name: "fornecedors_cad");
        }
    }
}
