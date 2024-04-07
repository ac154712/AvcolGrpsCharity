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
    public class CharityGrpStaffsController : Controller
    {
        private readonly AvcolGrpsCharityDbContext _context;

        public CharityGrpStaffsController(AvcolGrpsCharityDbContext context)
        {
            _context = context;
        }

        // GET: CharityGrpStaffs
        public async Task<IActionResult> Index()
        {
            var avcolGrpsCharityDbContext = _context.CharityGrpStaff.Include(c => c.SignedCharityGrps);
            return View(await avcolGrpsCharityDbContext.ToListAsync());
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
            ViewData["SignedCharityGrpId"] = new SelectList(_context.SignedCharityGrps, "CharityGrpID", "CharityGrp_description");
            return View();
        }

        // POST: CharityGrpStaffs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StaffmemberID,StaffMember_name,StaffMember_email,StaffMember_phonenum,SignedCharityGrpId")] CharityGrpStaff charityGrpStaff)
        {
            if (ModelState.IsValid)
            {
                _context.Add(charityGrpStaff);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SignedCharityGrpId"] = new SelectList(_context.SignedCharityGrps, "CharityGrpID", "CharityGrp_description", charityGrpStaff.SignedCharityGrpId);
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
            ViewData["SignedCharityGrpId"] = new SelectList(_context.SignedCharityGrps, "CharityGrpID", "CharityGrp_description", charityGrpStaff.SignedCharityGrpId);
            return View(charityGrpStaff);
        }

        // POST: CharityGrpStaffs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StaffmemberID,StaffMember_name,StaffMember_email,StaffMember_phonenum,SignedCharityGrpId")] CharityGrpStaff charityGrpStaff)
        {
            if (id != charityGrpStaff.StaffmemberID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
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
            ViewData["SignedCharityGrpId"] = new SelectList(_context.SignedCharityGrps, "CharityGrpID", "CharityGrp_description", charityGrpStaff.SignedCharityGrpId);
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
