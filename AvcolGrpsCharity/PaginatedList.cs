using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Threading.Tasks; 
using AvcolGrpsCharity.Models; 
using Microsoft.EntityFrameworkCore; 

namespace AvcolGrpsCharity
{
    public class PaginatedList<T> : List<T> // defines a generic class that inherits from List<T>
    {
        public int PageIndex { get; private set; } // this is the current page index
        public int TotalPages { get; private set; } // Totalnumber of pages

        public PaginatedList(List<T> items, int count, int pageIndex, int pageSize) // Constructor to initialize the PaginatedList
        {
            PageIndex = pageIndex; // sets the current page index
            TotalPages = (int)Math.Ceiling(count / (double)pageSize); // calculates total pages based on count and page size

            this.AddRange(items); // Adds the items to the list
        }

        public bool HasPreviousPage => PageIndex > 1; // a property to check if there is a previous page

        public bool HasNextPage => PageIndex < TotalPages; // Property to check if there is a next page

        // Asynchronous method to create a paginated list
        public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize)
        {
            var count = await source.CountAsync(); // gets the total count of items
            var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync(); // gets the items for the current page
            return new PaginatedList<T>(items, count, pageIndex, pageSize); // returns a new PaginatedList with the items, count, page index, and page size
        }
    }
}
