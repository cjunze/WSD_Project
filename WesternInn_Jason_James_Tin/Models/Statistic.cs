using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace WesternInn_Jason_James_Tin.Models
{
    public class Statistic
    {
        [Display(Name = "Postcode")]
        public string PostCode { get; set; } = string.Empty;
        [Display(Name = "Number of Guests")]
        public int GuestCount { get; set; }

        [Display(Name = "Room ID")]
        public int RoomID { get; set; }
        [Display(Name = "Number of Bookings")]
        public int BookingCount { get; set; }
    }
}
