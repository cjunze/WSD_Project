using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Data;
using WesternInn_Jason_James_Tin.Models;

namespace WesternInn_Jason_James_Tin.Pages.Bookings
{
    [Authorize(Roles = "guests")]
    public class MyBookingsModel : PageModel
    {
        private readonly WesternInn_Jason_James_Tin.Data.ApplicationDbContext _context;

        public MyBookingsModel(WesternInn_Jason_James_Tin.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Booking> Booking { get; set; } = default!;

        public async Task OnGetAsync(string? sortOrder)
        {
            if (string.IsNullOrEmpty(sortOrder))
            {
                sortOrder = "checkIn_asc";
            }

            var bookings = (IQueryable<Booking>)_context.Booking;

            switch (sortOrder) 
            {
                case "checkIn_asc":
                    bookings = bookings.OrderBy(m => m.CheckIn);
                    break;
                case "checkIn_desc":
                    bookings = bookings.OrderByDescending(m => m.CheckIn);
                    break;
                case "price_asc":
                    bookings = bookings.OrderBy(m => (double)m.Cost);
                    break;
                case "price_desc":
                    bookings = bookings.OrderByDescending(m => (double)m.Cost);
                    break;
            }

            ViewData["NextCheckInOrder"] = sortOrder != "checkIn_asc" ? "checkIn_asc" : "checkIn_desc";
            ViewData["NextPriceOrder"] = sortOrder != "price_asc" ? "price_asc" : "price_desc";

            Booking = await bookings.AsNoTracking().Include(p => p.TheGuest).ToListAsync();
        }
        //public void OnGet()
        //{
        //}
    }
}
