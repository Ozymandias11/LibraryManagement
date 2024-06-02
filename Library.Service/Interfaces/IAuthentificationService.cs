using Library.Service.Dto;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Library.Service.Interfaces
{
   public interface IAuthentificationService
    {
        Task<(IdentityResult result, string emailConfirmationToken)> RegisterEmployee(RegisterViewModelDto registerViewModel);
        Task<IdentityResult> LoginEmployee(LoginViewModelDto loginViewModel);
        Task<IdentityResult> LogoutEmployee();
        Task<IdentityResult> ResetPassword(ResetPasswordViewModelDto resetPasswordViewModel);
        Task<IdentityResult> ConfirmEmail(string token, string email);
        Task<IdentityResult> AddAdmin(CreateAdminViewModelDto createAdminViewModelDto);
        Task<bool> ValidateToken(string id, string Token);
        Task<string> ForgotPassword(string email);
        Task<string> ChangeEmail(string oldEmail, string newEmail);
        Task<bool> ConfirmEmailChange(string currentUserEmail, string token, string email);
        Task<IdentityResult> UpdateEmail(string oldEmail, string newEmail, string token);
    }
}
