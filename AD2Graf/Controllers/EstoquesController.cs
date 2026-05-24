using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AD2Graf.Data;
using AD2Graf.Models;
namespace AD2Graf.Controllers
{
    public class EstoquesController : Controller
    {
        private readonly AD2GrafContext _context;

        public EstoquesController(AD2GrafContext context)
        {
            _context = context;
        }

        // GET: Estoques
        public async Task<IActionResult> Index()
        {
            var estoques = await _context.Estoque
                .Include(e => e.Insumo)
                .ToListAsync();

            // Busca a data da última movimentação por InsumoId
            var ultimasMovimentacoes = await _context.Movimentacao
                .GroupBy(m => m.InsumoId)
                .Select(g => new { InsumoId = g.Key, UltimaData = g.Max(m => m.DataMovimentacao) })
                .ToListAsync();

            ViewBag.UltimasMovimentacoes = ultimasMovimentacoes
                .ToDictionary(x => x.InsumoId, x => x.UltimaData);

            return View(estoques);
        }

        // GET: Estoques/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var estoque = await _context.Estoque
                .Include(e => e.Insumo)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (estoque == null) return NotFound();

            return View(estoque);
        }

        // GET: Estoques/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Estoques/Create
        // Recebe NomeInsumo como texto livre; cria o Insumo se ainda não existir.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("DataCadastro,QuantidadeEstoque,PrecoUnitario")] Estoque estoque,
            string NomeInsumo)
        {
            if (string.IsNullOrWhiteSpace(NomeInsumo))
                ModelState.AddModelError("NomeInsumo", "O nome do insumo é obrigatório.");

            if (ModelState.IsValid)
            {
                // Verifica se já existe um insumo com esse nome (case-insensitive)
                var insumo = await _context.Insumo
                    .FirstOrDefaultAsync(i => i.Nome.ToLower() == NomeInsumo.Trim().ToLower());

                // Se não existir, cria
                if (insumo == null)
                {
                    insumo = new Insumo { Nome = NomeInsumo.Trim() };
                    _context.Insumo.Add(insumo);
                    await _context.SaveChangesAsync();
                }

                estoque.InsumoId = insumo.Id;
                _context.Estoque.Add(estoque);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(estoque);
        }

        // GET: Estoques/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var estoque = await _context.Estoque
                .Include(e => e.Insumo)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (estoque == null) return NotFound();

            return View(estoque);
        }

        // POST: Estoques/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,
            [Bind("Id,DataCadastro,InsumoId,QuantidadeEstoque,PrecoUnitario")] Estoque estoque)
        {
            if (id != estoque.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(estoque);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EstoqueExists(estoque.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(estoque);
        }

        // GET: Estoques/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var estoque = await _context.Estoque
                .Include(e => e.Insumo)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (estoque == null) return NotFound();

            return View(estoque);
        }

        // POST: Estoques/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var estoque = await _context.Estoque.FindAsync(id);
            if (estoque != null)
            {
                // Remove o estoque e o insumo vinculado
                var insumo = await _context.Insumo.FindAsync(estoque.InsumoId);
                _context.Estoque.Remove(estoque);
                if (insumo != null)
                    _context.Insumo.Remove(insumo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EstoqueExists(int id)
        {
            return _context.Estoque.Any(e => e.Id == id);
        }
    }
}