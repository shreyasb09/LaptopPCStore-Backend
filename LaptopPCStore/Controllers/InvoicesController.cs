using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LaptopPCStore.Models;
using Microsoft.AspNetCore.Authorization;

namespace LaptopPCStore.Controllers
{
    [Authorize]
    public class InvoicesController : Controller
    {
        private readonly LaptopStoreContext _context;

        public InvoicesController(LaptopStoreContext context)
        {
            _context = context;
        }

        // GET: Invoices
        public async Task<IActionResult> Index()
        {
            var laptopStoreContext = _context.invoices.Include(i => i.fk6);
            return View(await laptopStoreContext.ToListAsync());
        }

        // GET: Invoices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _context.invoices
                .Include(i => i.fk6)
                .FirstOrDefaultAsync(m => m.invoice_id == id);
            if (invoice == null)
            {
                return NotFound();
            }

            return View(invoice);
        }

        // GET: Invoices/Create
        public IActionResult Create()
        {
            ViewData["lap_id"] = new SelectList(_context.laptops, "lap_id", "lap_name");
            return View();
        }

        // POST: Invoices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("invoice_id,userID,lap_id,quantity,inv_name,inv_phone,inv_mail,inv_address")] Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                if (InventoryExists(invoice.lap_id))
                {
                    var inventories = _context.inventories.Find(invoice.lap_id);
                    if (invoice.quantity > inventories.quantity) 
                    {
                        ViewData["lap_id"] = new SelectList(_context.laptops, "lap_id", "lap_name", invoice.lap_id);
                        return View(invoice);
                    }
                    else 
                    {
                        _context.Add(invoice);
                        await _context.SaveChangesAsync();

                        inventories.quantity -= invoice.quantity;
                        try
                        {
                            _context.Update(inventories);
                            await _context.SaveChangesAsync();
                        }
                        catch (DbUpdateConcurrencyException)
                        {
                            if (!InventoryExists(inventories.lap_id))
                            {
                                return NotFound();
                            }
                            else
                            {
                                throw;
                            }
                        }
                    } 
                    
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["lap_id"] = new SelectList(_context.laptops, "lap_id", "lap_name", invoice.lap_id);
            return View(invoice);
        }

        // GET: Invoices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _context.invoices.FindAsync(id);
            if (invoice == null)
            {
                return NotFound();
            }
            ViewData["lap_id"] = new SelectList(_context.laptops, "lap_id", "lap_name", invoice.lap_id);
            return View(invoice);
        }

        // POST: Invoices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("invoice_id,userID,lap_id,quantity,inv_name,inv_phone,inv_mail,inv_address")] Invoice invoice)
        {
            if (id != invoice.invoice_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(invoice);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InvoiceExists(invoice.invoice_id))
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
            ViewData["lap_id"] = new SelectList(_context.laptops, "lap_id", "lap_name", invoice.lap_id);
            return View(invoice);
        }

        // GET: Invoices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _context.invoices
                .Include(i => i.fk6)
                .FirstOrDefaultAsync(m => m.invoice_id == id);
            if (invoice == null)
            {
                return NotFound();
            }

            return View(invoice);
        }

        // POST: Invoices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var invoice = await _context.invoices.FindAsync(id);
            _context.invoices.Remove(invoice);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InvoiceExists(int id)
        {
            return _context.invoices.Any(e => e.invoice_id == id);
        }
        private bool InventoryExists(int id)
        {
            return _context.inventories.Any(e => e.lap_id == id);
        }
    }
}
