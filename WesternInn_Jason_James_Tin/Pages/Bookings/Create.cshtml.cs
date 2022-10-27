using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using WesternInn_Jason_James_Tin.Data;
using WesternInn_Jason_James_Tin.Models;

namespace WesternInn_Jason_James_Tin.Pages.Bookings
{
    [Authorize(Roles = "administrators")]
    public class CreateModel : PageModel
    {
        private readonly WesternInn_Jason_James_Tin.Data.ApplicationDbContext _context;

        public CreateModel(WesternInn_Jason_James_Tin.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["GuestEmail"] = new SelectList(_context.Guest, "Email", "FullName");
        ViewData["RoomID"] = new SelectList(_context.Room, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public Booking Booking { get; set; }
        public IList<Booking> BookingResult { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid)
            {
                return Page();
            }

            var roomIdInput = new SqliteParameter("roomId", Booking.RoomID);
            var checkInInput = new SqliteParameter("checkIn", Booking.CheckIn);
            var checkOutInput = new SqliteParameter("checkOut", Booking.CheckOut);

            // this query search for overlap bookings with intended checkin or checkout date, if there has 1 booking -> cannot insert.
            var listRoom = _context.Booking.FromSqlRaw("select [Booking].* from [Booking] where [Booking].RoomID = @roomId "
                                                       + " and ([Booking].CheckIn < @checkOut and @checkIn < [Booking].CheckOut)"
                                                    , roomIdInput, checkInInput, checkOutInput);

            BookingResult = await listRoom.ToListAsync();

            if (BookingResult.Count > 0)
            {
                ViewData["SuccessBooking"] = false;
                return Page();
            }
            else
            {
                _context.Booking.Add(Booking);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
        }
    }
}
