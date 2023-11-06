using System;
using Microsoft.EntityFrameworkCore.Migrations;

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
                    EmpresaId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Document = table.Column<string>(type: "TEXT", nullable: false),
                    Uf = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_empresa_cad", x => x.EmpresaId);
                });

            migrationBuilder.CreateTable(
                name: "fornecedors_cad",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Document = table.Column<string>(type: "TEXT", nullable: false),
                    DataCad = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Rg = table.Column<string>(type: "TEXT", nullable: true),
                    DataNasc = table.Column<DateOnly>(type: "TEXT", nullable: true),
                    Uf = table.Column<string>(type: "TEXT", nullable: false),
                    TipoFornecedor = table.Column<string>(type: "TEXT", nullable: false),
                    EmpresaVinculada = table.Column<int>(type: "INTEGER", nullable: false),
                    Telefone_1 = table.Column<string>(type: "TEXT", nullable: false),
                    Telefone_2 = table.Column<string>(type: "TEXT", nullable: false),
                    Celular = table.Column<string>(type: "TEXT", nullable: false)
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
