using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InventoryManagement.Data;
using InventoryManagement.Models;

namespace InventoryManagement.Controllers
{
    public class ComputersController : Controller
    {
        private readonly InventoryManagementContext _context;

        public ComputersController(InventoryManagementContext context)
        {
            _context = context;
        }

        // GET: Computers
        public async Task<IActionResult> Index(bool clear, string searchSerialNumber, 
                        string roomNumber, DateTime? dateRangeStart, DateTime? dateRangeEnd,
                        string priceRangeStart, string priceRangeEnd)
        {
            var computers = from c in _context.Computer
                            select c;
            if (clear) { }
            if (searchSerialNumber != null)
                computers = computers.Where(c => c.manufacturerSerialNumber == int.Parse(searchSerialNumber));
            if (roomNumber != null)
                computers = computers.Where(c => c.OfficeRoomNumber!.Contains(roomNumber));
            if (dateRangeStart != null && dateRangeEnd != null)
            {
                computers = computers.Where(c => c.InstallationDate > dateRangeStart 
                            && c.InstallationDate < dateRangeEnd);
            }
            if (priceRangeStart != null && priceRangeEnd != null)
            {
                computers = computers.Where(c => c.Price > decimal.Parse(priceRangeStart)
                            && c.Price < decimal.Parse(priceRangeEnd));
            }

            return View(await computers.ToListAsync());
              //return View(await _context.Computer.ToListAsync());
        }

        // GET: Computers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Computer == null)
            {
                return NotFound();
            }

            var computer = await _context.Computer
                .FirstOrDefaultAsync(m => m.id == id);
            if (computer == null)
            {
                return NotFound();
            }

            return View(computer);
        }

        // GET: Computers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Computers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,manufacturerSerialNumber,OfficeRoomNumber,OfficeLocation,ComputerSpecification,OperatingSystem,OwnerName,InstallationDate,Price")] Computer computer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(computer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(computer);
        }

        // GET: Computers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Computer == null)
            {
                return NotFound();
            }

            var computer = await _context.Computer.FindAsync(id);
            if (computer == null)
            {
                return NotFound();
            }
            return View(computer);
        }

        // POST: Computers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,manufacturerSerialNumber,OfficeRoomNumber,OfficeLocation,ComputerSpecification,OperatingSystem,OwnerName,InstallationDate,Price")] Computer computer)
        {
            if (id != computer.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(computer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComputerExists(computer.id))
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
            return View(computer);
        }

        // GET: Computers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Computer == null)
            {
                return NotFound();
            }

            var computer = await _context.Computer
                .FirstOrDefaultAsync(m => m.id == id);
            if (computer == null)
            {
                return NotFound();
            }

            return View(computer);
        }

        // POST: Computers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Computer == null)
            {
                return Problem("Entity set 'InventoryManagementContext.Computer'  is null.");
            }
            var computer = await _context.Computer.FindAsync(id);
            if (computer != null)
            {
                _context.Computer.Remove(computer);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ComputerExists(int id)
        {
          return _context.Computer.Any(e => e.id == id);
        }
    }
}
