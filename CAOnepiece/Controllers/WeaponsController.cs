using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CAOnepiece.Data;
using CAOnepiece.Models;

namespace CAOnepiece.Controllers
{
    public class WeaponsController : Controller
    {
        private readonly CAOnepieceContext _context;

        public WeaponsController(CAOnepieceContext context)
        {
            _context = context;
        }

        // GET: Weapons

        // GET: Weapons/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var weapons = await _context.Weapons
                .FirstOrDefaultAsync(m => m.Id == id);
            if (weapons == null)
            {
                return NotFound();
            }

            return View(weapons);
        }

        // GET: weapons/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: weapons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,WeeaponName,Description,BossId")] Weapon weapons)
        {
            if (ModelState.IsValid)
            {
                _context.Add(weapons);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(weapons);
        }

        // GET: weapons/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var weapons = await _context.Weapons.FindAsync(id);
            if (weapons == null)
            {
                return NotFound();
            }
            return View(weapons);
        }

        // POST: weapons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,WeeaponName,Description,BossId")] Weapon weapons)
        {
            if (id != weapons.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(weapons);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WeaponExist(weapons.Id))
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
            return View(weapons);
        }

        // GET: Weapons/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var weapons = await _context.Weapons
                .FirstOrDefaultAsync(m => m.Id == id);
            if (weapons == null)
            {
                return NotFound();
            }

            return View(weapons);
        }


        public async Task<IActionResult> Index(string weapontype, string searchString)
        {
            if (_context.Weapons == null)
            {
                return Problem("Entity set 'MvcMovieContext.Movie'  is null.");
            }

            // Use LINQ to get list of genres.
            IQueryable<string> genreQuery = from m in _context.Weapons
                                            orderby m.WeaponName
                                            select m.WeaponName;
            var weapons = from m in _context.Weapons
                       select m;

            if (!string.IsNullOrEmpty(searchString))
            {
                weapons = weapons.Where(s => s.WeaponName!.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(weapontype))
            {
                weapons = weapons.Where(x => x.Description == weapontype);
            }

            var WeapontypeVm = new WeaponsTypeViewModel
            {
                Type = new SelectList(await genreQuery.Distinct().ToListAsync()),
                weapons = await weapons.ToListAsync()
            };

            return View(WeapontypeVm);
        }

        // POST: Weapons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var Weapons = await _context.Weapons.FindAsync(id);
            if (Weapons != null)
            {
                _context.Weapons.Remove(Weapons);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WeaponExist(int id)
        {
            return _context.Boss.Any(e => e.Id == id);
        }
    }
}
