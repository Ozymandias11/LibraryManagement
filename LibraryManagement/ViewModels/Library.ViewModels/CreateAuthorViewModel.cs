using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.ViewModels.Library.ViewModels
{
    public class CreateAuthorViewModel
    {

        [Required(ErrorMessage = "The First Name is Required")]
        public string? FirstName { get; set; }
        [Required(ErrorMessage = "The Last Name is Required")]
        public string? LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
