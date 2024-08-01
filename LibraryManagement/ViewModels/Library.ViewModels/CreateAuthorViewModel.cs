using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.ViewModels.Library.ViewModels
{
    public class CreateAuthorViewModel
    {

        [Required(ErrorMessage = "FirstNameRequired")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "LastNameRequired")]
        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }
    }
}
