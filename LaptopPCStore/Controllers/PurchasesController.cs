using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LaptopPCStore.Models;

namespace LaptopPCStore.Controllers
{
    public class PurchasesController : Controller
    {
        private readonly LaptopStoreContext _context;

        public PurchasesController(LaptopStoreContext context)
        {
            _context = context;
        }

        // GET: Purchases
        public async Task<IActionResult> Index()
        {
            var laptopStoreContext = _context.purchases.Include(p => p.fk2).Include(p => p.fk3);
            return View(await laptopStoreContext.ToListAsync());
        }

        // GET: Purchases/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchase = await _context.purchases
                .Include(p => p.fk2)
                .Include(p => p.fk3)
                .FirstOrDefaultAsync(m => m.purchase_id == id);
            if (purchase == null)
            {
                return NotFound();
            }

            return View(purchase);
        }

        // GET: Purchases/Create
        public IActionResult Create()
        {
            ViewData["ven_id"] = new SelectList(_context.vendors, "ven_id", "ven_name");
            ViewData["lap_id"] = new SelectList(_context.laptops, "lap_id", "lap_name");
            return View();
        }

        // POST: Purchases/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("purchase_id,ven_id,lap_id,purchase_quantity,purchase_date,purchase_cost")] Purchase purchase)
        {
            if (ModelState.IsValid)
            {
                _context.Add(purchase);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ven_id"] = new SelectList(_context.vendors, "ven_id", "ven_name", purchase.ven_id);
            ViewData["lap_id"] = new SelectList(_context.laptops, "lap_id", "lap_name", purchase.lap_id);
            return View(purchase);
        }

        // GET: Purchases/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchase = await _context.purchases.FindAsync(id);
            if (purchase == null)
            {
                return NotFound();
            }
            ViewData["ven_id"] = new SelectList(_context.vendors, "ven_id", "ven_name", purchase.ven_id);
            ViewData["lap_id"] = new SelectList(_context.laptops, "lap_id", "lap_name", purchase.lap_id);
            return View(purchase);
        }

        // POST: Purchases/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("purchase_id,ven_id,lap_id,purchase_quantity,purchase_date,purchase_cost")] Purchase purchase)
        {
            if (id != purchase.purchase_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(purchase);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PurchaseExists(purchase.purchase_id))
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
            ViewData["ven_id"] = new SelectList(_context.vendors, "ven_id", "ven_name", purchase.ven_id);
            ViewData["lap_id"] = new SelectList(_context.laptops, "lap_id", "lap_name", purchase.lap_id);
            return View(purchase);
        }

        // GET: Purchases/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchase = await _context.purchases
                .Include(p => p.fk2)
                .Include(p => p.fk3)
                .FirstOrDefaultAsync(m => m.purchase_id == id);
            if (purchase == null)
            {
                return NotFound();
            }

            return View(purchase);
        }

        // POST: Purchases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var purchase = await _context.purchases.FindAsync(id);
            _context.purchases.Remove(purchase);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PurchaseExists(int id)
        {
            return _context.purchases.Any(e => e.purchase_id == id);
        }
    }
}
