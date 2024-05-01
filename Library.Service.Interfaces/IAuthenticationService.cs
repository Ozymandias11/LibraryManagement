using Microsoft.AspNetCore.Identity;
using Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Library.Service.Interfaces
{
    public interface IAuthenticationService
    {
        Task<IdentityResult> RegisterEmployee(RegisterViewModel registerViewModel);
        Task<IdentityResult> LoginEmployee(LoginViewModel loginViewModel);
        Task<IdentityResult> LogoutEmployee();
        Task<IdentityResult> ResetPassword(ResetPasswordViewModel resetPasswordViewModel);
    }
}
