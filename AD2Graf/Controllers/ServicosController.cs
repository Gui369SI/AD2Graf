using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AD2Graf.Models;
using AD2Graf.Data;

namespace AD2Graf.Controllers
{
    public class ServicosController : Controller
    {
        private readonly AD2GrafContext _context;

        public ServicosController(AD2GrafContext context)
        {
            _context = context;
        }

        // GET: SERVICOS
        public async Task<IActionResult> Index()
        {
            return View(await _context.Servico.ToListAsync());
        }

        // GET: SERVICOS/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var servico = await _context.Servico.FirstOrDefaultAsync(m => m.Id == id);
            if (servico == null) return NotFound();

            return View(servico);
        }

        // GET: SERVICOS/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SERVICOS/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string Nome, decimal PrecoServico, DateTime DataCadastro)
        {
            // Verifica se já existe serviço com este nome (case-insensitive)
            var jaExiste = await _context.Servico.AnyAsync(s =>
                s.Nome.ToLower() == Nome.ToLower());
            if (jaExiste)
            {
                ModelState.AddModelError("Nome", "Já existe um serviço com este nome.");
                return View();
            }

            var servico = new Servico { Nome = Nome, PrecoServico = PrecoServico, DataCadastro = DataCadastro };
            _context.Add(servico);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: SERVICOS/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var servico = await _context.Servico.FindAsync(id);
            if (servico == null) return NotFound();

            return View(servico);
        }

        // POST: SERVICOS/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, string Nome, decimal PrecoServico, DateTime DataCadastro)
        {
            if (id == null) return NotFound();

            var servico = await _context.Servico.FindAsync(id);
            if (servico == null) return NotFound();

            // Validação de duplicata (exceto o próprio)
            var jaExiste = await _context.Servico.AnyAsync(s =>
                s.Nome.ToLower() == Nome.ToLower() && s.Id != id);
            if (jaExiste)
            {
                ModelState.AddModelError("Nome", "Já existe um serviço com este nome.");
                return View(servico);
            }

            servico.Nome = Nome;
            servico.PrecoServico = PrecoServico;
            servico.DataCadastro = DataCadastro;
            _context.Update(servico);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: SERVICOS/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var servico = await _context.Servico.FirstOrDefaultAsync(m => m.Id == id);
            if (servico == null) return NotFound();

            return View(servico);
        }

        // POST: SERVICOS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var servico = await _context.Servico.FindAsync(id);
            if (servico != null)
            {
                _context.Servico.Remove(servico);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ServicoExists(int? id)
        {
            return _context.Servico.Any(e => e.Id == id);
        }
    }
}