
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
        if (id == null)
        {
            return NotFound();
        }

        var insumo = await _context.Insumo
            .FirstOrDefaultAsync(m => m.Id == id);
        if (insumo == null)
        {
            return NotFound();
        }

        return View(insumo);
    }

    // GET: INSUMOS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: INSUMOS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Nome")] Insumo insumo)
    {
        if (ModelState.IsValid)
        {
            _context.Add(insumo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(insumo);
    }

    // GET: INSUMOS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var insumo = await _context.Insumo.FindAsync(id);
        if (insumo == null)
        {
            return NotFound();
        }
        return View(insumo);
    }

    // POST: INSUMOS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,Nome")] Insumo insumo)
    {
        if (id != insumo.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(insumo);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InsumoExists(insumo.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(insumo);
    }

    // GET: INSUMOS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var insumo = await _context.Insumo
            .FirstOrDefaultAsync(m => m.Id == id);
        if (insumo == null)
        {
            return NotFound();
        }

        return View(insumo);
    }

    // POST: INSUMOS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var insumo = await _context.Insumo.FindAsync(id);
        if (insumo != null)
        {
            _context.Insumo.Remove(insumo);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool InsumoExists(int? id)
    {
        return _context.Insumo.Any(e => e.Id == id);
    }
}
}
