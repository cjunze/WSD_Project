using System.ComponentModel.DataAnnotations;

namespace WesternInn_Jason_James_Tin.Models
{
    public class SearchRoomInput
    {
        [Required]
        [Display(Name = "Number of Beds")]
        public int BedCountInput { get; set; }

        [Display(Name = "Check-in Date")]
        [DataType(DataType.Date)]
        [Required]
        public DateTime? CheckInInput { get; set; }

        [Display(Name = "Check-out Date")]
        [DataType(DataType.Date)]
        [Required]
        public DateTime? CheckOutInput { get; set; }
    }
}
