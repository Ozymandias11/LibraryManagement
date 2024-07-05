using Library.Model.Models;

namespace LibraryManagement.ViewModels.Library.ViewModels
{
    public class CustomerViewModel
    {
        public Guid CustomerId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }

        public string? Address { get; set; }
    }
}
