using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AD2Graf.Data;
using AD2Graf.Models;

namespace AD2Graf.Controllers
{
    public class MovimentacoesController : Controller
    {
        private readonly AD2GrafContext _context;

        public MovimentacoesController(AD2GrafContext context)
        {
            _context = context;
        }

        // GET: Movimentacoes
        public async Task<IActionResult> Index()
        {
            var movimentacoes = await _context.Movimentacao
                .Include(m => m.Insumo)
                .OrderByDescending(m => m.DataMovimentacao)
                .ToListAsync();

            return View(movimentacoes);
        }

        // GET: Movimentacoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var movimentacao = await _context.Movimentacao
                .Include(m => m.Insumo)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (movimentacao == null) return NotFound();

            return View(movimentacao);
        }

        // GET: Movimentacoes/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.InsumoId = new SelectList(
                await _context.Insumo.Where(i => i.Ativo).ToListAsync(), "Id", "Nome");
            return View();
        }

        // POST: Movimentacoes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Id,InsumoId,TipoMovimentacao,Quantidade,DataMovimentacao")] Movimentacao movimentacao)
        {
            if (ModelState.IsValid)
            {
                // Verifica se é uma saída e se deixaria o saldo negativo
                if (movimentacao.TipoMovimentacao == TipoMovimentacao.Saida)
                {
                    var estoque = await _context.Estoque
                        .FirstOrDefaultAsync(e => e.InsumoId == movimentacao.InsumoId);

                    if (estoque != null && (estoque.QuantidadeEstoque - movimentacao.Quantidade) < 0)
                    {
                        ModelState.AddModelError("Quantidade",
                            $"Quantidade insuficiente. Saldo disponível: {estoque.QuantidadeEstoque}");
                        ViewBag.InsumoId = new SelectList(
                            await _context.Insumo.Where(i => i.Ativo).ToListAsync(), "Id", "Nome", movimentacao.InsumoId);
                        return View(movimentacao);
                    }
                }

                // 1. Salva a movimentação
                _context.Add(movimentacao);
                await _context.SaveChangesAsync();

                // 2. Atualiza a quantidade no Estoque correspondente
                var estoqueAtualizar = await _context.Estoque
                    .FirstOrDefaultAsync(e => e.InsumoId == movimentacao.InsumoId);

                if (estoqueAtualizar != null)
                {
                    if (movimentacao.TipoMovimentacao == TipoMovimentacao.Entrada)
                        estoqueAtualizar.QuantidadeEstoque += (int)movimentacao.Quantidade;
                    else if (movimentacao.TipoMovimentacao == TipoMovimentacao.Saida)
                        estoqueAtualizar.QuantidadeEstoque -= (int)movimentacao.Quantidade;

                    _context.Update(estoqueAtualizar);
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction(nameof(Index));
            }

            ViewBag.InsumoId = new SelectList(
                await _context.Insumo.Where(i => i.Ativo).ToListAsync(), "Id", "Nome", movimentacao.InsumoId);
            return View(movimentacao);
        }

        // GET: Movimentacoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var movimentacao = await _context.Movimentacao.FindAsync(id);
            if (movimentacao == null) return NotFound();

            ViewBag.InsumoId = new SelectList(await _context.Insumo.ToListAsync(), "Id", "Nome", movimentacao.InsumoId);
            return View(movimentacao);
        }

        // POST: Movimentacoes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,
            [Bind("Id,InsumoId,TipoMovimentacao,Quantidade,DataMovimentacao")] Movimentacao movimentacao)
        {
            if (id != movimentacao.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movimentacao);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovimentacaoExists(movimentacao.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }

            ViewBag.InsumoId = new SelectList(await _context.Insumo.ToListAsync(), "Id", "Nome", movimentacao.InsumoId);
            return View(movimentacao);
        }

        // GET: Movimentacoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var movimentacao = await _context.Movimentacao
                .Include(m => m.Insumo)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (movimentacao == null) return NotFound();

            return View(movimentacao);
        }

        // POST: Movimentacoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movimentacao = await _context.Movimentacao.FindAsync(id);
            if (movimentacao != null)
            {
                // Reverte o efeito da movimentação no estoque
                var estoque = await _context.Estoque
                    .FirstOrDefaultAsync(e => e.InsumoId == movimentacao.InsumoId);

                if (estoque != null)
                {
                    // Calcula qual seria o novo saldo após reverter
                    int novoSaldo = estoque.QuantidadeEstoque;
                    if (movimentacao.TipoMovimentacao == TipoMovimentacao.Entrada)
                        novoSaldo -= (int)movimentacao.Quantidade;
                    else if (movimentacao.TipoMovimentacao == TipoMovimentacao.Saida)
                        novoSaldo += (int)movimentacao.Quantidade;

                    // Bloqueia se deixaria negativo
                    if (novoSaldo < 0)
                    {
                        TempData["Erro"] = "Não é possível deletar esta movimentação pois deixaria o saldo negativo.";
                        return RedirectToAction(nameof(Index));
                    }

                    estoque.QuantidadeEstoque = novoSaldo;
                    _context.Update(estoque);
                }

                _context.Movimentacao.Remove(movimentacao);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovimentacaoExists(int id)
        {
            return _context.Movimentacao.Any(e => e.Id == id);
        }
    }
}