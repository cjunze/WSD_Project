using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace WesternInn_Jason_James_Tin.Models
{
    public class BookRoomInput
    {
        [Required]
        [Display(Name = "Room ID")]
        public int RoomIdInput { get; set; }
        [Display(Name = "Check-in Date")]
        [DataType(DataType.Date)]
        public DateTime CheckInInput { get; set; }

        [Display(Name = "Check-out Date")]
        [DataType(DataType.Date)]
        public DateTime CheckOutInput { get; set; }
    }
}
