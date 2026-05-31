using AD2Graf.Data;
using AD2Graf.Models;
using Microsoft.EntityFrameworkCore;

namespace AD2Graf.Repositorios
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly AD2GrafContext _context;

        public PedidoRepository(AD2GrafContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Pedido>> ListarTodosAsync()
            => await _context.Pedidos
                .Include(p => p.Servico)
                .OrderByDescending(p => p.DataCriacao)
                .ToListAsync();

        public async Task<Pedido?> BuscarPorIdAsync(int id)
            => await _context.Pedidos.FindAsync(id);

        public async Task AdicionarAsync(Pedido pedido)
            => await _context.Pedidos.AddAsync(pedido);

        public async Task AtualizarAsync(Pedido pedido)
            => _context.Pedidos.Update(pedido);

        public async Task RemoverAsync(Pedido pedido)
            => _context.Pedidos.Remove(pedido);

        public async Task SalvarAsync()
            => await _context.SaveChangesAsync();
    }
}