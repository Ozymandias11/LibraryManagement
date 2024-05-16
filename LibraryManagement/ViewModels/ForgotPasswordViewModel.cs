﻿using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.ViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "Email is a required Field")]
        public string? Email {  get; set; } 
    }
}
