﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AvcolGrpsCharity.Areas.Identity.Data;
using AvcolGrpsCharity.Models;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;

namespace AvcolGrpsCharity.Controllers
{
    public class DonorsController : Controller
    {
        private readonly AvcolGrpsCharityDbContext _context;

        public DonorsController(AvcolGrpsCharityDbContext context)
        {
            _context = context;
        }

        // GET: Donors
        public async Task<IActionResult> Index(string sortOrder, string searchString, string currentFilter, int? pageNumber)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            
            ViewData["CurrentSort"] = sortOrder;

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewData["CurrentFilter"] = searchString;

            var donors = from s in _context.Donors
                                    select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                donors = donors.Where(s => s.Donor_name.Contains(searchString)
                                       || s.Donor_email.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    donors = donors.OrderByDescending(s => s.Donor_name);
                    break;
                case "":
                    donors = donors.OrderBy(s => s.Donor_email);
                    break;
            }

            int pageSize = 5;

            return View(await PaginatedList<Donors>.CreateAsync(donors.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Donors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donors = await _context.Donors
                .Include(d => d.SignedCharityGrps)
                .FirstOrDefaultAsync(m => m.DonorID == id);
            if (donors == null)
            {
                return NotFound();
            }

            return View(donors);
        }

        // GET: Donors/Create
        public IActionResult Create()
        {
            ViewData["SignedCharityGrpId"] = new SelectList(_context.SignedCharityGrps, "CharityGrpID", "CharityGrpID");
            return View();
        }

        // POST: Donors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("DonorID,Donor_name,Donor_email,SignedCharityGrpId")] Donors donors)
        {
            if (!ModelState.IsValid)
            {
                _context.Add(donors);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SignedCharityGrpId"] = new SelectList(_context.SignedCharityGrps, "CharityGrpID", "CharityGrpID", donors.SignedCharityGrpId);
            return View(donors);
        }

        // GET: Donors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donors = await _context.Donors.FindAsync(id);
            if (donors == null)
            {
                return NotFound();
            }
            ViewData["SignedCharityGrpId"] = new SelectList(_context.SignedCharityGrps, "CharityGrpID", "CharityGrpID", donors.SignedCharityGrpId);
            return View(donors);
        }

        // POST: Donors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("DonorID,Donor_name,Donor_email,SignedCharityGrpId")] Donors donors)
        {
            if (id != donors.DonorID)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(donors);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DonorsExists(donors.DonorID))
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
            ViewData["SignedCharityGrpId"] = new SelectList(_context.SignedCharityGrps, "CharityGrpID", "CharityGrpID", donors.SignedCharityGrpId);
            return View(donors);
        }

        // GET: Donors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donors = await _context.Donors
                .Include(d => d.SignedCharityGrps)
                .FirstOrDefaultAsync(m => m.DonorID == id);
            if (donors == null)
            {
                return NotFound();
            }

            return View(donors);
        }

        // POST: Donors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var donors = await _context.Donors.FindAsync(id);
            if (donors != null)
            {
                _context.Donors.Remove(donors);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DonorsExists(int id)
        {
            return _context.Donors.Any(e => e.DonorID == id);
        }
    }
}
