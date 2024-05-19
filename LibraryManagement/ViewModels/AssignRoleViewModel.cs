using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.ViewModels
{
    public class AssignRoleViewModel
    {

        public string Id { get; set; }  

        [Required(ErrorMessage = "Please select at least one role.")]
        public ICollection<string>? SelectedRoles { get; set; }

        public IEnumerable<string> AvailableRoles { get; set; } = new List<string>
    {
        "Administrator",
        "Manager",
        "Librarian"
    };
    }
}
