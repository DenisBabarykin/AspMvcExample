using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AspMvcExample.Models;

namespace AspMvcExample.Controllers
{
    public class ReceiptsController : Controller
    {
        private readonly TradingContext _context;

        public ReceiptsController(TradingContext context)
        {
            _context = context;
        }

        // GET: Receipts
        public async Task<IActionResult> Index()
        {
            var TradingContext = _context.Receipts.Include(r => r.Client);
            return View(await TradingContext.ToListAsync());
        }

        // GET: Receipts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var receipts = await _context.Receipts
                .Include(r => r.Client)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (receipts == null)
            {
                return NotFound();
            }

            return View(receipts);
        }

        // GET: Receipts/Create
        public IActionResult Create()
        {
            ViewData["ClientId"] = new SelectList(_context.Clients, "Id", "Name");
            return View();
        }

        // POST: Receipts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ClientId,Time,Cost")] Receipt receipts)
        {
            if (ModelState.IsValid)
            {
                _context.Add(receipts);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClientId"] = new SelectList(_context.Clients, "Id", "Name", receipts.ClientId);
            return View(receipts);
        }

        // GET: Receipts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var receipts = await _context.Receipts.FindAsync(id);
            if (receipts == null)
            {
                return NotFound();
            }
            ViewData["ClientId"] = new SelectList(_context.Clients, "Id", "Name", receipts.ClientId);
            return View(receipts);
        }

        // POST: Receipts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ClientId,Time,Cost")] Receipt receipts)
        {
            if (id != receipts.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(receipts);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReceiptsExists(receipts.Id))
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
            ViewData["ClientId"] = new SelectList(_context.Clients, "Id", "Name", receipts.ClientId);
            return View(receipts);
        }

        // GET: Receipts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var receipts = await _context.Receipts
                .Include(r => r.Client)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (receipts == null)
            {
                return NotFound();
            }

            return View(receipts);
        }

        // POST: Receipts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var receipts = await _context.Receipts.FindAsync(id);
            _context.Receipts.Remove(receipts);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReceiptsExists(int id)
        {
            return _context.Receipts.Any(e => e.Id == id);
        }
    }
}
