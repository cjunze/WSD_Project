using System.ComponentModel.DataAnnotations;

namespace WesternInn_Jason_James_Tin.Models
{
    public class Booking
    {
        public int Id { get; set; }

        [Display(Name = "Room ID")]
        public int RoomID { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string GuestEmail { get; set; } = string.Empty;

        [Display(Name = "Check-in Date")]
        [DataType(DataType.Date)]
        public DateTime CheckIn { get; set; }

        [Display(Name = "Check-out Date")]
        [DataType(DataType.Date)]
        public DateTime CheckOut { get; set; }

        [Range(0, 10000)]
        [DataType(DataType.Currency)]
        public decimal Cost { get; set; }

        public Room? TheRoom { get; set; }
        public Guest? TheGuest { get; set; }
    }
}
