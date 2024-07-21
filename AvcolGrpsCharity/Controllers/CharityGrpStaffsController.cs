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
    public class CharityGrpStaffsController : Controller
    {
        private readonly AvcolGrpsCharityDbContext _context;

        public CharityGrpStaffsController(AvcolGrpsCharityDbContext context)
        {
            _context = context;
        }

        // GET: CharityGrpStaffs
        public async Task<IActionResult> Index(string sortOrder, string searchString, string currentFilter,
    int? pageNumber)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            ViewData["CurrentFilter"] = searchString;

            var signedCharityGrps = from s in _context.CharityGrpStaff
                                    select s;

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            if (!String.IsNullOrEmpty(searchString) )
            {
                signedCharityGrps = signedCharityGrps.Where(s => s.StaffMember_name.Contains(searchString)
                                       || s.StaffMember_name.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    signedCharityGrps = signedCharityGrps.OrderByDescending(s => s.StaffMember_name);
                    break;
                case "":
                    signedCharityGrps = signedCharityGrps.OrderBy(s => s.StaffMember_name);
                    break;
            }
            int pageSize = 5;

            return View(await PaginatedList<SignedCharityGrps>.CreateAsync((IQueryable<SignedCharityGrps>)signedCharityGrps.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: CharityGrpStaffs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var charityGrpStaff = await _context.CharityGrpStaff
                .Include(c => c.SignedCharityGrps)
                .FirstOrDefaultAsync(m => m.StaffmemberID == id);
            if (charityGrpStaff == null)
            {
                return NotFound();
            }

            return View(charityGrpStaff);
        }

        // GET: CharityGrpStaffs/Create
        public IActionResult Create()
        {
            ViewData["SignedCharityGrpId"] = new SelectList(_context.SignedCharityGrps, "CharityGrpID", "CharityGrpID");
            return View();
        }

        // POST: CharityGrpStaffs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("StaffmemberID,StaffMember_name,StaffMember_email,StaffMember_phonenum,SignedCharityGrpId")] CharityGrpStaff charityGrpStaff)
        {
            if (!ModelState.IsValid)
            {
                _context.Add(charityGrpStaff);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SignedCharityGrpId"] = new SelectList(_context.SignedCharityGrps, "CharityGrpID", "CharityGrpID", charityGrpStaff.SignedCharityGrpId);
            return View(charityGrpStaff);
        }

        // GET: CharityGrpStaffs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var charityGrpStaff = await _context.CharityGrpStaff.FindAsync(id);
            if (charityGrpStaff == null)
            {
                return NotFound();
            }
            ViewData["SignedCharityGrpId"] = new SelectList(_context.SignedCharityGrps, "CharityGrpID", "CharityGrpID", charityGrpStaff.SignedCharityGrpId);
            return View(charityGrpStaff);
        }

        // POST: CharityGrpStaffs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("StaffmemberID,StaffMember_name,StaffMember_email,StaffMember_phonenum,SignedCharityGrpId")] CharityGrpStaff charityGrpStaff)
        {
            if (id != charityGrpStaff.StaffmemberID)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(charityGrpStaff);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CharityGrpStaffExists(charityGrpStaff.StaffmemberID))
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
            ViewData["SignedCharityGrpId"] = new SelectList(_context.SignedCharityGrps, "CharityGrpID", "CharityGrpID", charityGrpStaff.SignedCharityGrpId);
            return View(charityGrpStaff);
        }

        // GET: CharityGrpStaffs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var charityGrpStaff = await _context.CharityGrpStaff
                .Include(c => c.SignedCharityGrps)
                .FirstOrDefaultAsync(m => m.StaffmemberID == id);
            if (charityGrpStaff == null)
            {
                return NotFound();
            }

            return View(charityGrpStaff);
        }

        // POST: CharityGrpStaffs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var charityGrpStaff = await _context.CharityGrpStaff.FindAsync(id);
            if (charityGrpStaff != null)
            {
                _context.CharityGrpStaff.Remove(charityGrpStaff);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CharityGrpStaffExists(int id)
        {
            return _context.CharityGrpStaff.Any(e => e.StaffmemberID == id);
        }
    }
}
