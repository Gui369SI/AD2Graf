using Microsoft.EntityFrameworkCore;
using AD2Graf.Models;

namespace AD2Graf.Data
{
    public class AD2GrafContext : DbContext
    {
        public DbSet<AD2Graf.Models.Servico> Servico { get; set; } = default!;
        public AD2GrafContext(DbContextOptions<AD2GrafContext> options)
            : base(options)
        {
        }

        public DbSet<Insumo> Insumo { get; set; } = default!;
        public DbSet<Estoque> Estoque { get; set; } = default!;
        public DbSet<Movimentacao> Movimentacao { get; set; } = default!;
        public DbSet<Pedido> Pedidos { get; set; } = default!;
    }
}