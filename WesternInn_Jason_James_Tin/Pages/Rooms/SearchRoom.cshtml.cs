using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        // Requires 'using Microsoft.AspNetCore.Mvc.Rendering;'

        public SelectList ListBedCount { get; set; } = default!;
        [BindProperty(SupportsGet = true)]
        public Room Room { get; set; } = default!;
        public Booking Booking { get; set; } = default!;

        public IActionResult OnGet()
        {
            var listBedCount = new List<int> { 1, 2, 3 };
            ListBedCount = new SelectList(listBedCount);
            // Obtain values for the <select> list in web form
            //stBedCount = new SelectList(_context.Room, "BedCount", "BedCount");
            // Display the page.
            return Page();
        }
        //public void OnGet()
        //{
        //}
    }
}
