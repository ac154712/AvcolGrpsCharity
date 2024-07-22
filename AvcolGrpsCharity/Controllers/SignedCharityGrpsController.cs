using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AvcolGrpsCharity.Areas.Identity.Data;
using AvcolGrpsCharity.Models;
using System.Drawing.Printing;
using AvcolGrpsCharity;
using Microsoft.AspNetCore.Authorization;

namespace AvcolGrpsCharity.Controllers
{
    public class SignedCharityGrpsController : Controller
    {
        private readonly AvcolGrpsCharityDbContext _context;

        public SignedCharityGrpsController(AvcolGrpsCharityDbContext context) // Constructor to initialize the DbContext
        {
            _context = context;
        }

        // GET: SignedCharityGrps
        // This action method fetches and displays the list of signed charity groups, allowing sorting and searching.
        public async Task<IActionResult> Index(string sortOrder, string searchString, string currentFilter, int? pageNumber)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : ""; // Sorting parameters for name
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date"; // sortsa parameters for date

            ViewData["CurrentSort"] = sortOrder; // preserves the current sort order

            if (searchString != null)  // If a new search string is provided, reset the page number to 1
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter; // Otherwise, use the current filter
            }

            ViewData["CurrentFilter"] = searchString; // stores the current filter value

            var signedCharityGrps = from s in _context.SignedCharityGrps select s; // fetches signed charity groups from the database

            if (!String.IsNullOrEmpty(searchString)) // filters the charity groups based on the search string
            {
                signedCharityGrps = signedCharityGrps.Where(s => s.ChartyGrp_Name.Contains(searchString)
                                       || s.CharityGrp_description.Contains(searchString));
            }

            switch (sortOrder) // sorts the charity groups based on the specified sort order
            {
                case "name_desc":
                    signedCharityGrps = signedCharityGrps.OrderByDescending(s => s.ChartyGrp_Name);
                    break;
                case "":
                    signedCharityGrps = signedCharityGrps.OrderBy(s => s.ChartyGrp_Name);
                    break;
            }

            int pageSize = 5;  // sets the page size for pagination

            return View(await PaginatedList<SignedCharityGrps>.CreateAsync(signedCharityGrps.AsNoTracking(), pageNumber ?? 1, pageSize)); // Returning the paginated list of charity groups to the view
        }

        // GET: SignedCharityGrps/Details/5
        // This action method fetches and displays the details of a specific signed charity group by ID.
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) // If no ID is provided, return a NotFound result
            {
                return NotFound();
            }

            var signedCharityGrps = await _context.SignedCharityGrps // Fetching the charity group details from the database
                .FirstOrDefaultAsync(m => m.CharityGrpID == id);
            if (signedCharityGrps == null)
            {
                return NotFound(); // If the charity group is not found, return a NotFound result
            }

            return View(signedCharityGrps); // Return the charity group details to the view
        }

        // GET: SignedCharityGrps/Create
        // This action method returns the view to create a new signed charity group.
        public IActionResult Create()
        {
            return View();
        }

        // POST: SignedCharityGrps/Create
        // This action method handles the creation of a new signed charity group and saves it to the database.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]  // This attribute ensures that only registered users can access this create method (minimum security requirements)
        public async Task<IActionResult> Create([Bind("CharityGrpID,ChartyGrp_Name,CharityGrp_description,CharityGrp_email,CharityGrp_phone")] SignedCharityGrps signedCharityGrps)
        {
            if (!ModelState.IsValid)  // If the model state is invalid
            {
                _context.Add(signedCharityGrps);    // adds the new charity group to the database
                await _context.SaveChangesAsync();   // saves changes asynchronously
                return RedirectToAction(nameof(Index));  // redirects to the index view
            }
            return View(signedCharityGrps);  // If the model state is valid, return the view with the charity group details
        }

        // GET: SignedCharityGrps/Edit/5
        // This action method fetches the details of a specific signed charity group for editing by ID.
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)  // If no ID is provided, return a NotFound result
            {
                return NotFound();
            }

            var signedCharityGrps = await _context.SignedCharityGrps.FindAsync(id); // Fetching the charity group details from the database
            if (signedCharityGrps == null)
            {
                return NotFound(); // If the charity group is not found, return a NotFound result
            }
            return View(signedCharityGrps); // returns the charity group details to the view for editing
        }

        // POST: SignedCharityGrps/Edit/5
        // This action method handles the updating of a signed charity group in the database.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("CharityGrpID,ChartyGrp_Name,CharityGrp_description,CharityGrp_email,CharityGrp_phone")] SignedCharityGrps signedCharityGrps)
        {
            if (id != signedCharityGrps.CharityGrpID) // If the provided ID does not match the charity group ID, return a NotFound result
            {
                return NotFound();
            }

            if (!ModelState.IsValid) // If the model state is invalid
            {
                try
                {
                    _context.Update(signedCharityGrps);  // Update the charity group in the database
                    await _context.SaveChangesAsync();  // Save changes asynchronously
                }
                catch (DbUpdateConcurrencyException) // catches concurrency exceptions
                {
                    if (!SignedCharityGrpsExists(signedCharityGrps.CharityGrpID)) // If the charity group does not exist, return a NotFound result
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw; // rethrows the exception if it's not a concurrency issue
                    }
                }
                return RedirectToAction(nameof(Index)); // redirects to the index view
            }
            return View(signedCharityGrps); // If the model state is valid, return the view with the charity group details
        }

        // GET: SignedCharityGrps/Delete/5
        // This action method fetches the details of a specific signed charity group for deletion by ID.
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) // If no ID is provided, return a NotFound result
            {
                return NotFound();
            }

            var signedCharityGrps = await _context.SignedCharityGrps // Fetching the charity group details from the database
                .FirstOrDefaultAsync(m => m.CharityGrpID == id);
            if (signedCharityGrps == null)
            {
                return NotFound(); // If the charity group is not found, return a NotFound result
            }

            return View(signedCharityGrps); // returns the charity group details to the view for confirmation of deletion
        }

        // POST: SignedCharityGrps/Delete/5
        // This action method handles the deletion of a signed charity group from the database.
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var signedCharityGrps = await _context.SignedCharityGrps.FindAsync(id); // Fetching the charity group details from the database
            if (signedCharityGrps != null)
            {
                _context.SignedCharityGrps.Remove(signedCharityGrps); // removes the charity group from the database
            }

            await _context.SaveChangesAsync(); // saves changes asynchronously
            return RedirectToAction(nameof(Index)); // redirects to the index view
        }

        // This method checks if a signed charity group exists in the database by ID.
        private bool SignedCharityGrpsExists(int id)
        {
            return _context.SignedCharityGrps.Any(e => e.CharityGrpID == id); // returns true if the charity group exists, otherwise false
        }
    }
}
