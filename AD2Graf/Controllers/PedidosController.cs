using AD2Graf.Models;
using AD2Graf.Servicos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AD2Graf.Data;

namespace AD2Graf.Controllers
{
    public class PedidosController : Controller
    {
        private readonly IPedidoService _service;
        private readonly AD2GrafContext _context;

        public PedidosController(IPedidoService service, AD2GrafContext context)
        {
            _service = service;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var pedidos = await _service.ListarPedidosAsync();
            return View(pedidos);
        }

        public async Task<IActionResult> Create()
        {
            // Carrega lista de serviços ativos
            var servicos = await _context.Servico.ToListAsync();
            ViewBag.Servicos = servicos;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Pedido pedido)
        {
            if (!ModelState.IsValid)
            {
                var servicos = await _context.Servico.ToListAsync();
                ViewBag.Servicos = servicos;
                return View(pedido);
            }

            // Pré-seleciona como Pendente se não foi definido
            pedido.Status = StatusPedido.Pendente;
            await _service.CriarPedidoAsync(pedido);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var pedido = await _service.BuscarPorIdAsync(id);
            if (pedido == null)
                return NotFound();

            // Carrega lista de serviços
            var servicos = await _context.Servico.ToListAsync();
            ViewBag.Servicos = servicos;

            return View(pedido);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Pedido pedido)
        {
            if (!ModelState.IsValid)
            {
                var servicos = await _context.Servico.ToListAsync();
                ViewBag.Servicos = servicos;
                return View(pedido);
            }

            await _service.AtualizarPedidoAsync(pedido);
            return RedirectToAction(nameof(Index));
        }

        // Nova action para atualizar status direto da Index
        [HttpPost]
        public async Task<IActionResult> AtualizarStatus(int id, StatusPedido novoStatus)
        {
            var pedido = await _service.BuscarPorIdAsync(id);
            if (pedido == null)
                return NotFound();

            pedido.Status = novoStatus;
            await _service.AtualizarPedidoAsync(pedido);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var pedido = await _context.Pedidos
                .Include(pedido => pedido.Servico)
                .FirstOrDefaultAsync(p => p.Id == id);

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