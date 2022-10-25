using System.ComponentModel.DataAnnotations;

namespace WesternInn_Jason_James_Tin.Models
{
    public class Room
    {
        public int Id { get; set; }

        [Required]
        [RegularExpression(@"^[G123]{1}$", ErrorMessage = "Sorry, the room level can only be either letter 'G', '1', '2' or '3'.")]
        public string Level { get; set; } = string.Empty;

        [Display(Name ="Number of Beds")]
        [RegularExpression(@"^[123]{1}$", ErrorMessage = "Sorry, the number of beds can only be in either 1, 2 or 3.")]
        public int BedCount { get; set; }

        [Display(Name = "Price Per Night")]
        [DataType(DataType.Currency)]
        [Range(50, 300)]
        public decimal Price { get; set; }

        public ICollection<Booking>? TheBookings { get; set; }
    }
}
