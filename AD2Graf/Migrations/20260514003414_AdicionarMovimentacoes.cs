using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AD2Graf.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarMovimentacoes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Movimentacao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoInsumo = table.Column<int>(type: "int", nullable: false),
                    TipoMovimentacao = table.Column<int>(type: "int", nullable: false),
                    Quantidade = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DataMovimentacao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movimentacao", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Movimentacao");
        }
    }
}
