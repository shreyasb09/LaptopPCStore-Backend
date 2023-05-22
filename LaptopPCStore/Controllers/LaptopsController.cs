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
    public class LaptopsController : Controller
    {
        private readonly LaptopStoreContext _context;

        public LaptopsController(LaptopStoreContext context)
        {
            _context = context;
        }

        // GET: Laptops
        public async Task<IActionResult> Index()
        {
            return View(await _context.laptops.ToListAsync());
        }

        // GET: Laptops/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var laptop = await _context.laptops
                .FirstOrDefaultAsync(m => m.lap_id == id);
            if (laptop == null)
            {
                return NotFound();
            }

            return View(laptop);
        }

        // GET: Laptops/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Laptops/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("lap_id,lap_name,lap_cat,lap_ram,lap_cpu,lap_gpu,lap_disp_size,lap_disp_res,lap_disp_rr,lap_battrey,lap_storage")] Laptop laptop)
        {
            if (ModelState.IsValid)
            {
                _context.Add(laptop);
                await _context.SaveChangesAsync();
                if (!InventoryExists(laptop.lap_id))
                {
                    try
                    {
                        var inventories = new Inventory
                        {
                            lap_id = laptop.lap_id,
                            quantity = 0
                        };
                        _context.inventories.Add(inventories);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {

                        throw;

                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(laptop);
        }

        // GET: Laptops/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var laptop = await _context.laptops.FindAsync(id);
            if (laptop == null)
            {
                return NotFound();
            }
            return View(laptop);
        }

        // POST: Laptops/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("lap_id,lap_name,lap_cat,lap_ram,lap_cpu,lap_gpu,lap_disp_size,lap_disp_res,lap_disp_rr,lap_battrey,lap_storage")] Laptop laptop)
        {
            if (id != laptop.lap_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(laptop);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LaptopExists(laptop.lap_id))
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
            return View(laptop);
        }

        // GET: Laptops/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var laptop = await _context.laptops
                .FirstOrDefaultAsync(m => m.lap_id == id);
            if (laptop == null)
            {
                return NotFound();
            }

            return View(laptop);
        }

        // POST: Laptops/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var laptop = await _context.laptops.FindAsync(id);
            _context.laptops.Remove(laptop);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LaptopExists(int id)
        {
            return _context.laptops.Any(e => e.lap_id == id);
        }
        private bool InventoryExists(int id)
        {
            return _context.inventories.Any(e => e.lap_id == id);
        }
    }
}
