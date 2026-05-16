using AD2Graf.Models;

namespace AD2Graf.Servicos
{
    public interface IPedidoService
    {
        Task<IEnumerable<Pedido>> ListarPedidosAsync();
        Task<Pedido?> BuscarPorIdAsync(int id);
        Task CriarPedidoAsync(Pedido pedido);
        Task AtualizarPedidoAsync(Pedido pedido);
        Task RemoverPedidoAsync(int id);
    }
}