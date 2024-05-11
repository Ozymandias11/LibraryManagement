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

        [Required(ErrorMessage = "Username is required")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        public string? PhoneNumber { get; set; }

        [Compare(nameof(Password), ErrorMessage = "Passwords do not match")]
        public string? ConfirmPassword { get; set; }
        [Required(ErrorMessage = "Please select at least one role.")]
        public ICollection<string>? SelectedRoles { get; set; }

        public IEnumerable<string> AvailableRoles { get; set; } = new List<string>
    {
        "Administrator",
        "Archivist",
        "Manager",
        "Librarian"
    };

    }
}
