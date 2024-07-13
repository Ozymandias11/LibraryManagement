using Library.Model.Models;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.ViewModels.Library.ViewModels
{
    public class UpdateCustomerViewModel
    {
        public Guid CustomerId { get; set; }    
        [Required(ErrorMessage = "First name is required")]
        [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [StringLength(50, ErrorMessage = "Last name cannot be longer than 50 characters")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [RegularExpression(@"^5\d{8}$", ErrorMessage = "Phone number must be in the format 5XXXXXXXX")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public Address Address { get; set; }
        [Required(ErrorMessage = "ID is required")]
        public string? CustomerPersonalId { get; set; }
    }
}
