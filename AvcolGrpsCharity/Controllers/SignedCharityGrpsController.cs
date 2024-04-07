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
    public class SignedCharityGrpsController : Controller
    {
        private readonly AvcolGrpsCharityDbContext _context;

        public SignedCharityGrpsController(AvcolGrpsCharityDbContext context)
        {
            _context = context;
        }

        // GET: SignedCharityGrps
        public async Task<IActionResult> Index()
        {
            return View(await _context.SignedCharityGrps.ToListAsync());
        }

        // GET: SignedCharityGrps/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var signedCharityGrps = await _context.SignedCharityGrps
                .FirstOrDefaultAsync(m => m.CharityGrpID == id);
            if (signedCharityGrps == null)
            {
                return NotFound();
            }

            return View(signedCharityGrps);
        }

        // GET: SignedCharityGrps/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SignedCharityGrps/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CharityGrpID,ChartyGrp_Name,CharityGrp_description,CharityGrp_email,CharityGrp_phone")] SignedCharityGrps signedCharityGrps)
        {
            if (ModelState.IsValid)
            {
                _context.Add(signedCharityGrps);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(signedCharityGrps);
        }

        // GET: SignedCharityGrps/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var signedCharityGrps = await _context.SignedCharityGrps.FindAsync(id);
            if (signedCharityGrps == null)
            {
                return NotFound();
            }
            return View(signedCharityGrps);
        }

        // POST: SignedCharityGrps/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CharityGrpID,ChartyGrp_Name,CharityGrp_description,CharityGrp_email,CharityGrp_phone")] SignedCharityGrps signedCharityGrps)
        {
            if (id != signedCharityGrps.CharityGrpID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(signedCharityGrps);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SignedCharityGrpsExists(signedCharityGrps.CharityGrpID))
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
            return View(signedCharityGrps);
        }

        // GET: SignedCharityGrps/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var signedCharityGrps = await _context.SignedCharityGrps
                .FirstOrDefaultAsync(m => m.CharityGrpID == id);
            if (signedCharityGrps == null)
            {
                return NotFound();
            }

            return View(signedCharityGrps);
        }

        // POST: SignedCharityGrps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var signedCharityGrps = await _context.SignedCharityGrps.FindAsync(id);
            if (signedCharityGrps != null)
            {
                _context.SignedCharityGrps.Remove(signedCharityGrps);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SignedCharityGrpsExists(int id)
        {
            return _context.SignedCharityGrps.Any(e => e.CharityGrpID == id);
        }
    }
}
