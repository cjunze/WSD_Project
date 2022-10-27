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
    public class EditModel : PageModel
    {
        private readonly WesternInn_Jason_James_Tin.Data.ApplicationDbContext _context;

        public EditModel(WesternInn_Jason_James_Tin.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Booking Booking { get; set; } = default!;
        public IList<Booking> BookingResult { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Booking == null)
            {
                return NotFound();
            }

            var booking =  await _context.Booking.FirstOrDefaultAsync(m => m.Id == id);
            if (booking == null)
            {
                return NotFound();
            }
            Booking = booking;
           ViewData["GuestEmail"] = new SelectList(_context.Guest, "Email", "FullName");
           ViewData["RoomID"] = new SelectList(_context.Room, "Id", "Id");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
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
                                                       + " and ([Booking].CheckIn < @checkOut and @checkIn < [Booking].CheckOut) "                                                     
                                                    , roomIdInput, checkInInput, checkOutInput);

            BookingResult = await listRoom.ToListAsync();

            if (BookingResult.Count > 0)
            {
                ViewData["SuccessBooking"] = false;
                return Page();
            } else
            {
                _context.Attach(Booking).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookingExists(Booking.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToPage("./Index");

            }

        }

        private bool BookingExists(int id)
        {
          return _context.Booking.Any(e => e.Id == id);
        }
    }
}
