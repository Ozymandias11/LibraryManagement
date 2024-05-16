using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.ViewModels
{
    public class CreateEmployeeViewModel
    {

        [Required(ErrorMessage = "First Name is a required field")]
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }    
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
