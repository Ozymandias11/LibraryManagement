using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.ViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "Email is a required Field")]
        [EmailAddress(ErrorMessage = "Input type is invalid")]
        public string? Email {  get; set; }
        public string? ErrorMessage { get; set; }
    }
}
