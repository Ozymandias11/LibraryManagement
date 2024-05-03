﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.ViewModels
{
    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "Email is Required")]
        [EmailAddress(ErrorMessage = "Invalid Email type")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "The password must contain at least 8 digits ")]
        public string? NewPassword { get; set; }

        [Required(ErrorMessage = "Confimation password is required")]
        [Compare(nameof(NewPassword), ErrorMessage = "Password did not match")]
        public string? ConfirmPassword { get; set; }
    }
}
