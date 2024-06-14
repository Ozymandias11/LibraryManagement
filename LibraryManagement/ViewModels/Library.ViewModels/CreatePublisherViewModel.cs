using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.ViewModels.Library.ViewModels
{
    public class CreatePublisherViewModel
    {
        [Required(ErrorMessage = "The First Name is Required")]
        public string? PublisherName { get; set; }
        [Required(ErrorMessage = "The phone Number is Required")]
        [RegularExpression(@"^[9][9][5][5][0-9]{8}$", ErrorMessage = "Phone number must be in the format 9955xxxxxxxx")]
        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Format")]
        public string? Email { get; set; }
    }
}
