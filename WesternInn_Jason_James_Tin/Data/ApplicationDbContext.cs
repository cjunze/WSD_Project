using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WesternInn_Jason_James_Tin.Models;

namespace WesternInn_Jason_James_Tin.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public DbSet<WesternInn_Jason_James_Tin.Models.Room> Room { get; set; }

        public DbSet<WesternInn_Jason_James_Tin.Models.Guest> Guest { get; set; }

        public DbSet<WesternInn_Jason_James_Tin.Models.Booking> Booking { get; set; }
    }

    /*  // the original
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
    */
}