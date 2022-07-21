using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using API_core2.Data;
using API_core2.Models;
using Microsoft.AspNetCore.Authorization;

namespace API_core2.Controllers
{
    public class idiomsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public idiomsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: idioms
        public async Task<IActionResult> Index()
        {
            return View(await _context.idioms.ToListAsync());
        }

        // GET: idioms/searchForm
        public async Task<IActionResult> ShowSearchForm()
        {
            return View();
        }

        // POST: idioms/ShowSearchResults
        public async Task<IActionResult> ShowSearchResults(string SearchPhrase)
        {
            return View("Index", await _context.idioms.Where(j => j.IdiomsQue.Contains(SearchPhrase)).ToListAsync());
        }

        // GET: idioms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var idioms = await _context.idioms
                .FirstOrDefaultAsync(m => m.Id == id);
            if (idioms == null)
            {
                return NotFound();
            }

            return View(idioms);
        }

        // GET: idioms/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: idioms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdiomsQue,IdiomsAns")] idioms idioms)
        {
            if (ModelState.IsValid)
            {
                _context.Add(idioms);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(idioms);
        }

        // GET: idioms/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var idioms = await _context.idioms.FindAsync(id);
            if (idioms == null)
            {
                return NotFound();
            }
            return View(idioms);
        }

        // POST: idioms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdiomsQue,IdiomsAns")] idioms idioms)
        {
            if (id != idioms.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(idioms);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!idiomsExists(idioms.Id))
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
            return View(idioms);
        }

        // GET: idioms/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var idioms = await _context.idioms
                .FirstOrDefaultAsync(m => m.Id == id);
            if (idioms == null)
            {
                return NotFound();
            }

            return View(idioms);
        }

        // POST: idioms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var idioms = await _context.idioms.FindAsync(id);
            _context.idioms.Remove(idioms);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool idiomsExists(int id)
        {
            return _context.idioms.Any(e => e.Id == id);
        }
    }
}
