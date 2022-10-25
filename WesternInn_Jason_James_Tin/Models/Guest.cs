using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WesternInn_Jason_James_Tin.Models
{
    public class Guest
    {
        [Key, Required]
        [DataType(DataType.EmailAddress)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MinLength(2), MaxLength(20)]
        [Display(Name ="Surname")]
        [RegularExpression(@"^[A-Za-z-']*$", ErrorMessage = "Sorry, the surname must consist of only English letters, hyphen and apostrophe and has a length between 2 and 20 characters inclusive")]
        public string Surname { get; set; } = string.Empty;

        [Required]
        [MinLength(2), MaxLength(20)]
        [Display(Name = "Given Name")]
        [RegularExpression(@"^[A-Za-z-']*$", ErrorMessage = "Sorry, the given name must consist of only English letters, hyphen and apostrophe and has a length between 2 and 20 characters inclusive")]
        public string GivenName { get; set; } = string.Empty;

        [Required]
        [RegularExpression(@"^[0-9]{4}$", ErrorMessage = "Sorry, the postcode should contain exactly 4 digits.")]
        public string PostCode { get; set; } = string.Empty;

        public ICollection<Booking>? TheBookings { get; set; }
    }
}
