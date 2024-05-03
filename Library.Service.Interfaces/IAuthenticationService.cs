﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Library.Service.Interfaces
{
    public interface IAuthenticationService
    {
        Task<IdentityResult> RegisterEmployee(RegisterViewModelDto registerViewModel);
        Task<IdentityResult> LoginEmployee(LoginViewModel loginViewModel);
        Task<IdentityResult> LogoutEmployee();
        Task<IdentityResult> ResetPassword(ResetPasswordViewModel resetPasswordViewModel);
    }
}
