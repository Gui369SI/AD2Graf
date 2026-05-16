using AD2Graf.Models;
using AD2Graf.Repositorios;

namespace AD2Graf.Servicos
{
    public class PedidoService : IPedidoService
    {
        private readonly IPedidoRepository _repository;

        public PedidoService(IPedidoRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Pedido>> ListarPedidosAsync()
            => await _repository.ListarTodosAsync();

        public async Task<Pedido?> BuscarPorIdAsync(int id)
            => await _repository.BuscarPorIdAsync(id);

        public async Task CriarPedidoAsync(Pedido pedido)
        {
            await _repository.AdicionarAsync(pedido);
            await _repository.SalvarAsync();
        }

        public async Task AtualizarPedidoAsync(Pedido pedido)
        {
            await _repository.AtualizarAsync(pedido);
            await _repository.SalvarAsync();
        }

        public async Task RemoverPedidoAsync(int id)
        {
            var pedido = await _repository.BuscarPorIdAsync(id);
            if (pedido != null)
            {
                await _repository.RemoverAsync(pedido);
                await _repository.SalvarAsync();
            }
        }
    }
}