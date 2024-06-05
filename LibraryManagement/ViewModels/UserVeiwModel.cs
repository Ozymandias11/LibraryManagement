using Library.Model.Models;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.ViewModels
{
    public class UserVeiwModel
    {
        

        public string Id { get; set; }  
        [Required(ErrorMessage = "FisrName is a required Field")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "LastName is a required Field")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is a requried field")]
        [EmailAddress(ErrorMessage = "Invalid format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [RegularExpression(@"^5\d{8}$", ErrorMessage = "Phone number must be in the format 5xxxxxxxx")]
        public string PhoneNumber { get; set; }
        public string Roles { get; set; }
        public bool EmailConfirmed { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? DeleteDate { get; set; }

    }
}
