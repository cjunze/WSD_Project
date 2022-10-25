using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;

namespace WesternInn_Jason_James_Tin.Pages.Bookings
{
    [Authorize(Roles = "administrators")]
    public class ManageBookingsModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
