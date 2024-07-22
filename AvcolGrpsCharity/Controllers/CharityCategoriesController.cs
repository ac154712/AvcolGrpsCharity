using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AvcolGrpsCharity.Areas.Identity.Data;
using AvcolGrpsCharity.Models;
using Microsoft.AspNetCore.Authorization;

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
        public async Task<IActionResult> Index(string sortOrder, string searchString, string currentFilter,
    int? pageNumber)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            ViewData["CurrentFilter"] = searchString;

            var charitycategory = from s in _context.CharityCategory
                                    select s;

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                charitycategory = charitycategory.Where(s => s.Category_name.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    charitycategory = charitycategory.OrderByDescending(s => s.Category_name);
                    break;
            }
            int pageSize = 5;

            return View(await PaginatedList<CharityCategory>.CreateAsync(charitycategory.AsNoTracking(), pageNumber ?? 1, pageSize));
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
        [Authorize]
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
        [Authorize]
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
        [Authorize]
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
