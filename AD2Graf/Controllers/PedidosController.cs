using AD2Graf.Models;
using AD2Graf.Servicos;
using Microsoft.AspNetCore.Mvc;

namespace AD2Graf.Controllers
{
    public class PedidosController : Controller
    {
        private readonly IPedidoService _service;

        public PedidosController(IPedidoService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var pedidos = await _service.ListarPedidosAsync();
            return View(pedidos);
        }

        public IActionResult Create()
            => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Pedido pedido)
        {
            if (!ModelState.IsValid)
                return View(pedido);

            await _service.CriarPedidoAsync(pedido);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var pedido = await _service.BuscarPorIdAsync(id);
            if (pedido == null)
                return NotFound();

            return View(pedido);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Pedido pedido)
        {
            if (!ModelState.IsValid)
                return View(pedido);

            await _service.AtualizarPedidoAsync(pedido);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var pedido = await _service.BuscarPorIdAsync(id);
            if (pedido == null)
                return NotFound();

            return View(pedido);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmado(int id)
        {
            await _service.RemoverPedidoAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }  
}
