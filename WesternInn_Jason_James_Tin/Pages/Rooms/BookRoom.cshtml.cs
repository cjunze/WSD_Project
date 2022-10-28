using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Globalization;
using System.Security.Claims;
using WesternInn_Jason_James_Tin.Models;


namespace WesternInn_Jason_James_Tin.Pages.Rooms
{
    [Authorize(Roles = "guests")]
    public class BookRoomModel : PageModel
    {
        private readonly WesternInn_Jason_James_Tin.Data.ApplicationDbContext _context;

        public BookRoomModel(WesternInn_Jason_James_Tin.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public BookRoomInput BookRoomInput { get; set; }
        public IList<Booking> BookingResult { get; set; }

        public Booking Booking { get; set; } = default!;

        
        public IActionResult OnGet()
        {
            ViewData["RoomIdList"] = new SelectList(_context.Room, "Id", "Id");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var roomIdInput = new SqliteParameter("roomId", BookRoomInput.RoomIdInput);
            var checkInInput = new SqliteParameter("checkIn", BookRoomInput.CheckInInput);
            var checkOutInput = new SqliteParameter("checkOut", BookRoomInput.CheckOutInput);

            // this query search for overlap bookings with intended checkin or checkout date, if there has 1 booking -> cannot insert.
            var listRoom = _context.Booking.FromSqlRaw("select [Booking].* from [Booking] where [Booking].RoomID = @roomId "
                                                       + " and ([Booking].CheckIn < @checkOut and @checkIn < [Booking].CheckOut)"
                                                    , roomIdInput, checkInInput, checkOutInput);

            BookingResult = await listRoom.ToListAsync();

            if (BookingResult.Count > 0)
            {
                return Page();
            } else
            {
                var emptyBooking = new Booking();

                var success = await TryUpdateModelAsync<Booking>(emptyBooking, "Booking",
                                    s => s.RoomID, s => s.GuestEmail, s => s.CheckIn, s => s.CheckOut, s => s.Cost);
                if (success)
                {
                    string _email = User.FindFirst(ClaimTypes.Name).Value;
                    emptyBooking.GuestEmail = _email;
                    emptyBooking.RoomID = BookRoomInput.RoomIdInput;
                    emptyBooking.CheckIn = (DateTime)BookRoomInput.CheckInInput;
                    emptyBooking.CheckOut = (DateTime)BookRoomInput.CheckOutInput;
                    var theRoom = await _context.Room.FindAsync(emptyBooking.RoomID);
                    var totalDays = (emptyBooking.CheckOut - emptyBooking.CheckIn).Days;
                    emptyBooking.Cost = totalDays * theRoom.Price;

                    _context.Booking.Add(emptyBooking);
                    await _context.SaveChangesAsync();
                    ViewData["SuccessBooking"] = "true";
                    ViewData["RoomID"] = emptyBooking.RoomID;
                    ViewData["Level"] = emptyBooking.TheRoom.Level;
                    ViewData["CheckIn"] = emptyBooking.CheckIn.ToShortDateString();
                    ViewData["CheckOut"] = emptyBooking.CheckOut.ToShortDateString();
                    ViewData["Cost"] = emptyBooking.Cost;
                    return Page();
                }
                else
                {
                    return Page();
                }

            }
        }

        //pu
        //blic void OnGet()
        //{
        //}

    }
}
