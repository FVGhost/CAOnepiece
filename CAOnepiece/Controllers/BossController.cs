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
    public class BossController : Controller
    {
        private readonly CAOnepieceContext _context;

        public BossController(CAOnepieceContext context)
        {
            _context = context;
        }

        // GET: Boss

        // GET: Boss/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var boss = await _context.Boss
                .FirstOrDefaultAsync(m => m.Id == id);
            if (boss == null)
            {
                return NotFound();
            }

            return View(boss);
        }

        // GET: Boss/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Boss/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BossName,Description")] Boss boss)
        {
            if (ModelState.IsValid)
            {
                _context.Add(boss);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(boss);
        }

        // GET: Boss/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var boss = await _context.Boss.FindAsync(id);
            if (boss == null)
            {
                return NotFound();
            }
            return View(boss);
        }

        // POST: Boss/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BossName,Description")] Boss boss)
        {
            if (id != boss.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(boss);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BossExist(boss.Id))
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
            return View(boss);
        }

        // GET: Boss/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var boss = await _context.Boss
                .FirstOrDefaultAsync(m => m.Id == id);
            if (boss == null)
            {
                return NotFound();
            }

            return View(boss);
        }


        public async Task<IActionResult> Index(string bosstype, string searchString)
        {
            if (_context.Boss == null)
            {
                return Problem("Entity set 'MvcMovieContext.Movie'  is null.");
            }

            // Use LINQ to get list of genres.
            IQueryable<string> genreQuery = from m in _context.Boss
                                            orderby m.BossName
                                            select m.BossName;
            var boss = from m in _context.Boss
                         select m;

            if (!string.IsNullOrEmpty(searchString))
            {
                boss = boss.Where(s => s.BossName!.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(bosstype))
            {
                boss = boss.Where(x => x.Description == bosstype);
            }

            var BosstypeVm = new BossTypeViewModel
            {
                Type = new SelectList(await genreQuery.Distinct().ToListAsync()),
                Boss = await boss.ToListAsync()
            };

            return View(BosstypeVm);
        }

        // POST: Boss/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var Boss = await _context.Boss.FindAsync(id);
            if (Boss != null)
            {
                _context.Boss.Remove(Boss);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BossExist(int id)
        {
            return _context.Boss.Any(e => e.Id == id);
        }
    }
}
