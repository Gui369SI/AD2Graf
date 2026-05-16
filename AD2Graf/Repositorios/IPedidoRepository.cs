using AD2Graf.Models;

namespace AD2Graf.Repositorios
{
    public interface IPedidoRepository
    {
        Task<IEnumerable<Pedido>> ListarTodosAsync();
        Task<Pedido?> BuscarPorIdAsync(int id);
        Task AdicionarAsync(Pedido pedido);
        Task AtualizarAsync(Pedido pedido);
        Task RemoverAsync(Pedido pedido);
        Task SalvarAsync();
    }
}
