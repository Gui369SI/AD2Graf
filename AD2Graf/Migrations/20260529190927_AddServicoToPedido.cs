using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AD2Graf.Migrations
{
    /// <inheritdoc />
    public partial class AddServicoToPedido : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Descricao",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "Preco",
                table: "Pedidos");

            migrationBuilder.AddColumn<string>(
                name: "ServicoId",
                table: "Pedidos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ServiçoId",
                table: "Pedidos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Servico",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrecoServico = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servico", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_ServiçoId",
                table: "Pedidos",
                column: "ServiçoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pedidos_Servico_ServiçoId",
                table: "Pedidos",
                column: "ServiçoId",
                principalTable: "Servico",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pedidos_Servico_ServiçoId",
                table: "Pedidos");

            migrationBuilder.DropTable(
                name: "Servico");

            migrationBuilder.DropIndex(
                name: "IX_Pedidos_ServiçoId",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "ServicoId",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "ServiçoId",
                table: "Pedidos");

            migrationBuilder.AddColumn<string>(
                name: "Descricao",
                table: "Pedidos",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "Preco",
                table: "Pedidos",
                type: "decimal(10,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
