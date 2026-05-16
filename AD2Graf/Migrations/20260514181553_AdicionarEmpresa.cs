using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AD2Graf.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarEmpresa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Empresa",
                table: "Pedidos",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Empresa",
                table: "Pedidos");
        }
    }
}
