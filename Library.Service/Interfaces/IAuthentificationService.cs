using Library.Service.Dto;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Service.Interfaces
{
   public interface IAuthentificationService
    {
        Task<IdentityResult> RegisterEmployee(RegisterViewModelDto registerViewModel);
        Task<IdentityResult> LoginEmployee(LoginViewModelDto loginViewModel);
        Task<IdentityResult> LogoutEmployee();
        Task<IdentityResult> ResetPassword(ResetPasswordViewModelDto resetPasswordViewModel);
        Task<IdentityResult> ConfirmEmail(string token, string userId);
        Task<IdentityResult> AddAdmin(CreateAdminViewModelDto createAdminViewModelDto);
        Task<bool> ValidateToken(string id, string Token);
    }
}
