using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AD2Graf.Models;
using AD2Graf.Data;

namespace AD2Graf.Controllers
{
    public class InsumosController : Controller
    {
        private readonly AD2GrafContext _context;

        public InsumosController(AD2GrafContext context)
        {
            _context = context;
        }

        // GET: INSUMOS
        public async Task<IActionResult> Index()
        {
            return View(await _context.Insumo.ToListAsync());
        }

        // GET: INSUMOS/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var insumo = await _context.Insumo.FirstOrDefaultAsync(m => m.Id == id);
            if (insumo == null) return NotFound();

            return View(insumo);
        }

        // GET: INSUMOS/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: INSUMOS/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string Nome, decimal PrecoUnitario)
        {
            if (string.IsNullOrWhiteSpace(Nome))
            {
                ModelState.AddModelError("Nome", "O nome do insumo é obrigatório.");
                return View();
            }

            var insumo = new Insumo { Nome = Nome, Ativo = true };
            _context.Insumo.Add(insumo);
            await _context.SaveChangesAsync();

            var estoque = new Estoque
            {
                InsumoId = insumo.Id,
                QuantidadeEstoque = 0,
                PrecoUnitario = PrecoUnitario,
                DataCadastro = DateTime.Now
            };
            _context.Estoque.Add(estoque);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: INSUMOS/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var insumo = await _context.Insumo.FindAsync(id);
            if (insumo == null) return NotFound();

            // Carrega o preço atual do estoque para exibir no form
            var estoque = await _context.Estoque.FirstOrDefaultAsync(e => e.InsumoId == id);
            ViewBag.PrecoUnitario = estoque?.PrecoUnitario ?? 0;

            return View(insumo);
        }

        // POST: INSUMOS/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, string Nome, decimal PrecoUnitario)
        {
            if (id == null) return NotFound();

            var insumo = await _context.Insumo.FindAsync(id);
            if (insumo == null) return NotFound();

            insumo.Nome = Nome;
            _context.Update(insumo);

            // Atualiza o preço no estoque
            var estoque = await _context.Estoque.FirstOrDefaultAsync(e => e.InsumoId == id);
            if (estoque != null)
            {
                estoque.PrecoUnitario = PrecoUnitario;
                _context.Update(estoque);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: INSUMOS/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var insumo = await _context.Insumo.FirstOrDefaultAsync(m => m.Id == id);
            if (insumo == null) return NotFound();

            return View(insumo);
        }

        // POST: INSUMOS/Delete/5 — alterna entre ativo e inativo
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var insumo = await _context.Insumo.FindAsync(id);
            if (insumo != null)
            {
                insumo.Ativo = !insumo.Ativo;
                _context.Update(insumo);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool InsumoExists(int? id)
        {
            return _context.Insumo.Any(e => e.Id == id);
        }
    }
}