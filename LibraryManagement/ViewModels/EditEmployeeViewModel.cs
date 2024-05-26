namespace LibraryManagement.ViewModels
{
    public class EditEmployeeViewModel
    {



        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Roles { get; set; }

        public IEnumerable<string> AvailableRoles { get; set; } = new List<string>
                {
                    "Administrator",
                    "Manager",
                    "Librarian"
                };

    }
}
