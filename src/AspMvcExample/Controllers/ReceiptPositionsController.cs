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
    public class ReceiptPositionsController : Controller
    {
        private readonly TradingContext _context;

        public ReceiptPositionsController(TradingContext context)
        {
            _context = context;
        }

        // GET: ReceiptPositions
        public async Task<IActionResult> Index()
        {
            var TradingContext = _context.ReceiptPositions.Include(r => r.Product).Include(r => r.Receipt);
            return View(await TradingContext.ToListAsync());
        }

        // GET: ReceiptPositions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var receiptPositions = await _context.ReceiptPositions
                .Include(r => r.Product)
                .Include(r => r.Receipt)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (receiptPositions == null)
            {
                return NotFound();
            }

            return View(receiptPositions);
        }

        // GET: ReceiptPositions/Create
        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name");
            ViewData["ReceiptId"] = new SelectList(_context.Receipts, "Id", "Id");
            return View();
        }

        // POST: ReceiptPositions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProductId,ReceiptId,Amount")] ReceiptPosition receiptPositions)
        {
            if (ModelState.IsValid)
            {
                _context.Add(receiptPositions);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name", receiptPositions.ProductId);
            ViewData["ReceiptId"] = new SelectList(_context.Receipts, "Id", "Id", receiptPositions.ReceiptId);
            return View(receiptPositions);
        }

        // GET: ReceiptPositions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var receiptPositions = await _context.ReceiptPositions.FindAsync(id);
            if (receiptPositions == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name", receiptPositions.ProductId);
            ViewData["ReceiptId"] = new SelectList(_context.Receipts, "Id", "Id", receiptPositions.ReceiptId);
            return View(receiptPositions);
        }

        // POST: ReceiptPositions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProductId,ReceiptId,Amount")] ReceiptPosition receiptPositions)
        {
            if (id != receiptPositions.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(receiptPositions);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReceiptPositionsExists(receiptPositions.Id))
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
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name", receiptPositions.ProductId);
            ViewData["ReceiptId"] = new SelectList(_context.Receipts, "Id", "Id", receiptPositions.ReceiptId);
            return View(receiptPositions);
        }

        // GET: ReceiptPositions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var receiptPositions = await _context.ReceiptPositions
                .Include(r => r.Product)
                .Include(r => r.Receipt)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (receiptPositions == null)
            {
                return NotFound();
            }

            return View(receiptPositions);
        }

        // POST: ReceiptPositions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var receiptPositions = await _context.ReceiptPositions.FindAsync(id);
            _context.ReceiptPositions.Remove(receiptPositions);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReceiptPositionsExists(int id)
        {
            return _context.ReceiptPositions.Any(e => e.Id == id);
        }
    }
}
