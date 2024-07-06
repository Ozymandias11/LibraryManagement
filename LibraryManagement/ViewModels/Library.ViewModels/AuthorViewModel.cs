using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.ViewModels.Library.ViewModels
{
    public class AuthorViewModel
    {
        public Guid AuthorId { get; set; }
        [Required(ErrorMessage = "First Name is a required field")]
        public string? FirstName { get; set; }
        [Required(ErrorMessage = "Last Name is a required field")]
        public string? LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? ErrorMessage { get; set; }    

    }
}
