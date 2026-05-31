using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AD2Graf.Migrations
{
    /// <inheritdoc />
    public partial class FixServicoType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pedidos_Servico_ServiçoId",
                table: "Pedidos");

            migrationBuilder.DropIndex(
                name: "IX_Pedidos_ServiçoId",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "ServiçoId",
                table: "Pedidos");

            migrationBuilder.AlterColumn<int>(
                name: "ServicoId",
                table: "Pedidos",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_ServicoId",
                table: "Pedidos",
                column: "ServicoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pedidos_Servico_ServicoId",
                table: "Pedidos",
                column: "ServicoId",
                principalTable: "Servico",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pedidos_Servico_ServicoId",
                table: "Pedidos");

            migrationBuilder.DropIndex(
                name: "IX_Pedidos_ServicoId",
                table: "Pedidos");

            migrationBuilder.AlterColumn<string>(
                name: "ServicoId",
                table: "Pedidos",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ServiçoId",
                table: "Pedidos",
                type: "int",
                nullable: true);

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
    }
}
