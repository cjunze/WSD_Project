using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Security.Claims;
using WesternInn_Jason_James_Tin.Models;

namespace WesternInn_Jason_James_Tin.Pages.Guests
{
    [Authorize(Roles = "guests")]

    public class MyDetailsModel : PageModel
    {
        private readonly WesternInn_Jason_James_Tin.Data.ApplicationDbContext _context;

        public MyDetailsModel(WesternInn_Jason_James_Tin.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Guest? Myself { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            // retrieve the logged-in user's email
            // need to add "using System.Security.Claims;"
            string _email = User.FindFirst(ClaimTypes.Name).Value;

            Guest guest = await _context.Guest.FirstOrDefaultAsync(m => m.Email == _email);

            /* dealing with both cases of new user and existing user */
            if (guest != null)
            {
                // existing user
                ViewData["ExistInDB"] = "true";
                Myself =guest;
            }
            else
            {
                // new user
                ViewData["ExistInDB"] = "false";
            }

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            // retrieve the logged-in user's email
            // need to add "using System.Security.Claims;"
            string _email = User.FindFirst(ClaimTypes.Name).Value;

            Guest guest = await _context.Guest.FirstOrDefaultAsync(m => m.Email == _email);

            /* dealing with both cases of new user and existing user */
            if (guest != null)
            {
                // This ViewData entry is needed in the content file
                // The user has been created in the database
                ViewData["ExistInDB"] = "true";
            }
            else
            {
                // new user
                ViewData["ExistInDB"] = "false";
                guest = new Guest();
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            /* for preventing overposting attacks */
            guest.Email = _email;

            /* movieGoer: the object to update using the values submitted.
             * "MovieGoer": the Prefix used in the input names in web form.
             * Lambda expression: list the properties to be updated. If not listed, 
             *                    no updates, thus preventing overposting.
             */
            var success = await TryUpdateModelAsync<Guest>(guest, "Myself",
                                s => s.Surname, s => s.GivenName, s => s.PostCode);
            if (!success)
            {
                return Page();
            }

            if ((string)ViewData["ExistInDB"] == "true")
            {
                // Since the context doesn't allow tracking two objects with the same key,
                // we do the copying first, and then update.
                _context.Guest.Update(guest);
            }
            else
            {
                // add this newly-created record into db
                _context.Guest.Add(guest);
            }

            try  // catching the conflict of editing this record concurrently
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            ViewData["SuccessDB"] = "success";
            return Page();
        }
    }
}
