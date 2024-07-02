
using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.ViewModels
{
    public class AssignRoleViewModel
    {

        public string Id { get; set; }

      //  [RequireAtLeastOneRole(ErrorMessage = "Please select at least one Role")]
        public ICollection<string>? SelectedRoles { get; set; }
        public ICollection<String>? CurrentRoles { get; set; }

        public string Email { get; set; }

        public IEnumerable<string> AvailableRoles { get; set; } = new List<string>
    {
        "Administrator",
        "Manager",
        "Librarian"
    };
    }
}
