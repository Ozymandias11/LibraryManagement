using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.ViewModels
{
    public class UserViewModelProfile
    {
        [Required(ErrorMessage = "FisrName is a required Field")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "LastName is a required Field")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is a requried field")]
        [EmailAddress(ErrorMessage = "Invalid format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [RegularExpression(@"^5\d{8}$", ErrorMessage = "Phone number must be in the format 5xxxxxxxx")]
        public string PhoneNumber { get; set; }
        public string Roles { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
