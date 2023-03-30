using invoice.Data;
using invoice.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace invoice.Controllers
{
    public class FeesController : Controller
    {
        private readonly ApplicationDbContext _context;
        public FeesController(ApplicationDbContext context)
        {
            _context = context;
        }
        //GET: Fees
        public async Task<IActionResult> Index()
        {
            var fees = await _context.Fees.ToListAsync();
            return _context.Fees != null ?
                View(fees) :
                Problem("There are no fees to display.");
        }
        // GET: Fees/Details/5
        public async Task<IActionResult> Details(ulong? id)
        {
            if (id == null || _context.Fees == null)
            {
                return NotFound();
            }

            var fee = await _context.Fees
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fee == null)
            {
                return NotFound();
            }

            return View(fee);
        }
        // GET: Fees/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Fees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Amount,IsFlat")] Fee fee)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(fee);
        }

        // GET: Fee/Edit/5
        public async Task<IActionResult> Edit(ulong? id)
        {
            if (id == null || _context.Fees == null)
            {
                return NotFound();
            }

            var fee = await _context.Fees.FindAsync(id);
            if (fee == null)
            {
                return NotFound();
            }
            return View(fee);
        }
        // POST: Fees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ulong id, [Bind("Id,Name,Amount,IsFlatFee")] Fee fee)
        {
            if (id != fee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FeeExists(fee.Id))
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
            return View(fee);
        }
        // GET: Fees/Delete/5
        public async Task<IActionResult> Delete(ulong? id)
        {
            if (id == null || _context.Fees == null)
            {
                return NotFound();
            }

            var fee = await _context.Fees
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fee == null)
            {
                return NotFound();
            }

            return View(fee);
        }
        // POST: Fees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(ulong id)
        {
            if (_context.Fees == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Fees'  is null.");
            }
            var fee = await _context.Fees.FindAsync(id);
            if (fee != null)
            {
                _context.Fees.Remove(fee);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool FeeExists(ulong id)
        {
            return (_context.Fees?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
