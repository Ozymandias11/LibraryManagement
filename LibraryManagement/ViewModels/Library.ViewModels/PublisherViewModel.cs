using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.ViewModels.Library.ViewModels
{
    public class PublisherViewModel
    {
        public Guid PublisherId { get; set; }
        [Required(ErrorMessage = "Publisher Name is Required")]
        public string? PublisherName { get; set; }
        [Required(ErrorMessage = "Publisher Phone is Required")]
        [RegularExpression(@"^5\d{8}$", ErrorMessage = "Phone number must be in the format 5XXXXXXXX")]
        public string? PhoneNumber { get; set; }
        [Required(ErrorMessage = "publisher Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address format")]
        public string? Email { get; set; }
    }
}
