using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.ViewModels
{
    public class RegisterViewModel
    {

        [Required(ErrorMessage = "First Name is required")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Format")]
        public string? Email { get; set; }
  
        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [RegularExpression(@"^[9][9][5][5][0-9]{8}$", ErrorMessage = "Phone number must be in the format 9955xxxxxxxx")]
        public string? PhoneNumber { get; set; }
        [Required(ErrorMessage = "The Birth Date is a required field")]
        public DateTime DateOfBirth { get; set; }

        [Compare(nameof(Password), ErrorMessage = "Passwords do not match")]
        public string? ConfirmPassword { get; set; }
        public string? ErrorMessage { get; set; }

    }
}
