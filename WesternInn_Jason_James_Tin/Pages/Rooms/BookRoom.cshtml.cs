using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;
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

        public SelectList ListBedCount { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public Room Room { get; set; } = default!;

        public IActionResult OnGet()
        {
            ListBedCount = new SelectList(_context.Room, "BedCount", "BedCount");
            return Page();
        }
        //public void OnGet()
        //{
        //}

        //public async Task<IActionResult> OnPostAsync()
        //{
        //    ListBedCount = new SelectList(_context.Room, "BedCount", "BedCount");

        //    if (!ModelState.IsValid || Room == null)
        //    {
        //        return Page();
        //    }


        //}
    }
}
