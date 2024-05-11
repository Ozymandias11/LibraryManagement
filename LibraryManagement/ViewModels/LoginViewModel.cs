using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.ViewModels
{
    public class LoginViewModel
    {

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is Required")]
       
        public string? Password { get; set; }
    }
}
