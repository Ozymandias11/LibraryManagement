using Library.Model.Models;

namespace LibraryManagement.ViewModels
{
    public class UserVeiwModel
    {
        

        public string Id { get; set; }  
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Roles { get; set; }
        public bool EmailConfirmed { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? DeleteDate { get; set; }

    }
}
