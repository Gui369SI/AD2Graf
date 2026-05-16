using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AD2Graf.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarInsumosDinamicos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            // Limpa dados antigos para permitir a criação das FKs
            migrationBuilder.Sql("DELETE FROM Movimentacao");
            migrationBuilder.Sql("DELETE FROM Estoque");
            migrationBuilder.RenameColumn(
                name: "TipoInsumo",
                table: "Movimentacao",
                newName: "InsumoId");

            migrationBuilder.RenameColumn(
                name: "Tipo",
                table: "Estoque",
                newName: "InsumoId");

            migrationBuilder.CreateTable(
                name: "Insumo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Insumo", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Movimentacao_InsumoId",
                table: "Movimentacao",
                column: "InsumoId");

            migrationBuilder.CreateIndex(
                name: "IX_Estoque_InsumoId",
                table: "Estoque",
                column: "InsumoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Estoque_Insumo_InsumoId",
                table: "Estoque",
                column: "InsumoId",
                principalTable: "Insumo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Movimentacao_Insumo_InsumoId",
                table: "Movimentacao",
                column: "InsumoId",
                principalTable: "Insumo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Estoque_Insumo_InsumoId",
                table: "Estoque");

            migrationBuilder.DropForeignKey(
                name: "FK_Movimentacao_Insumo_InsumoId",
                table: "Movimentacao");

            migrationBuilder.DropTable(
                name: "Insumo");

            migrationBuilder.DropIndex(
                name: "IX_Movimentacao_InsumoId",
                table: "Movimentacao");

            migrationBuilder.DropIndex(
                name: "IX_Estoque_InsumoId",
                table: "Estoque");

            migrationBuilder.RenameColumn(
                name: "InsumoId",
                table: "Movimentacao",
                newName: "TipoInsumo");

            migrationBuilder.RenameColumn(
                name: "InsumoId",
                table: "Estoque",
                newName: "Tipo");
        }
    }
}
