using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Data;
using WesternInn_Jason_James_Tin.Models;

namespace WesternInn_Jason_James_Tin.Pages.Rooms
{
    [Authorize(Roles = "guests")]
    public class SearchRoomModel : PageModel
    {
        private readonly WesternInn_Jason_James_Tin.Data.ApplicationDbContext _context;

        public SearchRoomModel(WesternInn_Jason_James_Tin.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public SearchRoomInput SearchRoomInput { get; set; }
        public IList<Room> RoomResult { get; set; }


        public IActionResult OnGet()
        {
            int[] BedCountRange = { 1, 2, 3 };
            List<int> listBedCount = new List<int>(BedCountRange);
            ViewData["BedCountList"] = new SelectList(listBedCount);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var bedCountInput = new SqliteParameter("bedCount", SearchRoomInput.BedCountInput);
            var checkInInput = new SqliteParameter("checkIn", SearchRoomInput.CheckInInput);
            var checkOutInput = new SqliteParameter("checkOut", SearchRoomInput.CheckOutInput);

            var listRoom = _context.Room.FromSqlRaw("select [Room].* from [Room] where [Room].BedCount = @bedCount "
                                                    + " and [Room].ID not in (select [Room].ID from [Room] inner join [Booking] on "
                                                           + "[Room].ID = [Booking].RoomID where [Booking].CheckIn < "
                                                           + "@checkOut and @checkIn < [Booking].CheckOut)", bedCountInput, checkInInput, checkOutInput);

            RoomResult = await listRoom.ToListAsync();

            return Page();
        }
        //public void OnGet()
        //{
        //}
    }
}
