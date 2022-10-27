using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Data;
using WesternInn_Jason_James_Tin.Models;

namespace WesternInn_Jason_James_Tin.Pages.Statistics
{
    [Authorize(Roles = "administrators")]
    public class StatisticsModel : PageModel
    {
        private readonly WesternInn_Jason_James_Tin.Data.ApplicationDbContext _context;

        public StatisticsModel(WesternInn_Jason_James_Tin.Data.ApplicationDbContext context)
        {
            _context = context;
        }
        public IList<Statistic> PostcodeStats { get; set; } = default!;
        public IList<Statistic> RoomStats { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            var postcodeGroups = _context.Guest.GroupBy(m => m.PostCode);

            PostcodeStats = await postcodeGroups
                             .Select(g => new Statistic { PostCode = g.Key, GuestCount = g.Count() })
                             .ToListAsync();

            var roomGroup = _context.Booking.GroupBy(m => m.RoomID);

            RoomStats = await roomGroup
                             .Select(g => new Statistic { RoomID = g.Key, BookingCount = g.Count() })
                             .ToListAsync();
            return Page();
        }
        /*public void OnGet()
        {
        }*/
    }
}
