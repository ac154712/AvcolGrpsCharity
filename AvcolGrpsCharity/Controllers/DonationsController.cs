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
    public class DonationsController : Controller
    {
        private readonly AvcolGrpsCharityDbContext _context;

        public DonationsController(AvcolGrpsCharityDbContext context)
        {
            _context = context;
        }

        // GET: Donations
        public async Task<IActionResult> Index(string sortOrder, string searchString, int? searchNo, string currentFilter,
    int? pageNumber)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            ViewData["CurrentFilter"] = searchNo;

            var donations = from d in _context.Donations
                                    select d;

            if (searchString != null || searchNo != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            if (searchNo.HasValue)
            {
                donations = donations.Where(d => d.DonationAmount == searchNo.Value);
            }
            switch (sortOrder)
            {
                case "name_desc":
                    donations = donations.OrderByDescending(d => d.DonationAmount);
                    break;
                case "":
                    donations = donations.OrderBy(d => d.DonationDate);
                    break;
                case "date_desc":
                    donations = donations.OrderByDescending(s => s.DonationDate);
                    break;
                default:
                    donations = donations.OrderBy(s => s.DonationAmount);
                    break;
            }
            int pageSize = 5;

            return View(await PaginatedList<Donations>.CreateAsync(donations.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Donations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donations = await _context.Donations
                .Include(d => d.Donors)
                .FirstOrDefaultAsync(m => m.DonationID == id);
            if (donations == null)
            {
                return NotFound();
            }

            return View(donations);
        }

        // GET: Donations/Create
        public IActionResult Create()
        {
            ViewData["DonorID"] = new SelectList(_context.Donors, "DonorID", "DonorID");
            return View();
        }

        // POST: Donations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("DonationID,DonationAmount,DonationMessage,DonationDate,DonorID")] Donations donations)
        {
            if (!ModelState.IsValid)
            {
                donations.DonationDate = DateTime.Now; // Set the current date

                _context.Add(donations);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DonorID"] = new SelectList(_context.Donors, "DonorID", "DonorID", donations.DonorID);
            return View(donations);
        }

        // GET: Donations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donations = await _context.Donations.FindAsync(id);
            if (donations == null)
            {
                return NotFound();
            }
            ViewData["DonorID"] = new SelectList(_context.Donors, "DonorID", "DonorID" , donations.DonorID);
            return View(donations);
        }

        // POST: Donations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("DonationID,DonationAmount,DonationMessage,DonationDate,DonorID")] Donations donations)
        {
            if (id != donations.DonationID)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(donations);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DonationsExists(donations.DonationID))
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
            ViewData["DonorID"] = new SelectList(_context.Donors, "DonorID", "DonorID", donations.DonorID);
            return View(donations);
        }

        // GET: Donations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donations = await _context.Donations
                .Include(d => d.Donors)
                .FirstOrDefaultAsync(m => m.DonationID == id);
            if (donations == null)
            {
                return NotFound();
            }

            return View(donations);
        }

        // POST: Donations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var donations = await _context.Donations.FindAsync(id);
            if (donations != null)
            {
                _context.Donations.Remove(donations);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DonationsExists(int id)
        {
            return _context.Donations.Any(e => e.DonationID == id);
        }
    }
}
