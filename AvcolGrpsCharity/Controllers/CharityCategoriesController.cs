using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AvcolGrpsCharity.Areas.Identity.Data;
using AvcolGrpsCharity.Models;

namespace AvcolGrpsCharity.Controllers
{
    public class CharityCategoriesController : Controller
    {
        private readonly AvcolGrpsCharityDbContext _context;

        public CharityCategoriesController(AvcolGrpsCharityDbContext context)
        {
            _context = context;
        }

        // GET: CharityCategories
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            ViewData["CurrentFilter"] = searchString;

            var signedCharityGrps = from s in _context.CharityCategory
                                    select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                signedCharityGrps = signedCharityGrps.Where(s => s.Category_name.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    signedCharityGrps = signedCharityGrps.OrderByDescending(s => s.Category_name);
                    break;
            }
            return View(await signedCharityGrps.AsNoTracking().ToListAsync());
        }

        // GET: CharityCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var charityCategory = await _context.CharityCategory
                .Include(c => c.SignedCharityGrps)
                .FirstOrDefaultAsync(m => m.CategoryID == id);
            if (charityCategory == null)
            {
                return NotFound();
            }

            return View(charityCategory);
        }

        // GET: CharityCategories/Create
        public IActionResult Create()
        {
            ViewData["SignedCharityGrpId"] = new SelectList(_context.SignedCharityGrps, "CharityGrpID", "CharityGrpID");
            return View();
        }

        // POST: CharityCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryID,Category_name,SignedCharityGrpId")] CharityCategory charityCategory)
        {
            if (!ModelState.IsValid)
            {
                _context.Add(charityCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SignedCharityGrpId"] = new SelectList(_context.SignedCharityGrps, "CharityGrpID", "CharityGrpID", charityCategory.SignedCharityGrpId);
            return View(charityCategory);
        }

        // GET: CharityCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var charityCategory = await _context.CharityCategory.FindAsync(id);
            if (charityCategory == null)
            {
                return NotFound();
            }
            ViewData["SignedCharityGrpId"] = new SelectList(_context.SignedCharityGrps, "CharityGrpID", "CharityGrpID", charityCategory.SignedCharityGrpId);
            return View(charityCategory);
        }

        // POST: CharityCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CategoryID,Category_name,SignedCharityGrpId")] CharityCategory charityCategory)
        {
            if (id != charityCategory.CategoryID)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(charityCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CharityCategoryExists(charityCategory.CategoryID))
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
            ViewData["SignedCharityGrpId"] = new SelectList(_context.SignedCharityGrps, "CharityGrpID", "CharityGrpID", charityCategory.SignedCharityGrpId);
            return View(charityCategory);
        }

        // GET: CharityCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var charityCategory = await _context.CharityCategory
                .Include(c => c.SignedCharityGrps)
                .FirstOrDefaultAsync(m => m.CategoryID == id);
            if (charityCategory == null)
            {
                return NotFound();
            }

            return View(charityCategory);
        }

        // POST: CharityCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var charityCategory = await _context.CharityCategory.FindAsync(id);
            if (charityCategory != null)
            {
                _context.CharityCategory.Remove(charityCategory);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CharityCategoryExists(int id)
        {
            return _context.CharityCategory.Any(e => e.CategoryID == id);
        }
    }
}
