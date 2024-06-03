using AutoMapper;
using Library.Model.Models;
using Library.Service.Dto;
using Library.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Library.Service
{
    public class AuthenticationService : IAuthentificationService
    {

        private readonly UserManager<Employee> _userManager;
        private readonly SignInManager<Employee> _signInManager;
        private readonly IMapper _mapper;


        public AuthenticationService(UserManager<Employee> userManager, 
            IMapper mapper, SignInManager<Employee> signInManager)
        {
            _userManager = userManager;
            _mapper = mapper;
            _signInManager = signInManager;
        }

        public async Task<bool> ConfirmEmailChange(string currentUserEmail,string token, string email)
        {

            var user = await _userManager.FindByEmailAsync(currentUserEmail);

            var isTokenValid = await _userManager.VerifyUserTokenAsync(
                           user,
                           _userManager.Options.Tokens.EmailConfirmationTokenProvider,
                           "ChangeEmailConfirmation",
                           token);

            return isTokenValid;


        }

        public async Task<IdentityResult> ConfirmEmail(string token, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            var result = await _userManager.ConfirmEmailAsync(user, token);

            return result;


        }

        public async Task<IdentityResult> LoginEmployee(LoginViewModelDto loginViewModel)
        {
            var existingUser = await _userManager.FindByEmailAsync(loginViewModel.Email);

            if(existingUser == null)
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Code = "UserNotFound",
                    Description = "User not found."
                });
            }

            // check if the user is deleted or not
            if(existingUser.DeleteDate != null)
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Code = "UserNotFound",
                    Description = "User not found."
                });
            }

            var result =  await _userManager.CheckPasswordAsync(existingUser, loginViewModel.Password);


            if (!result)
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Code = "IncorrectPassword",
                    Description = "Incorrect password."
                });
            }

            var signInResult = await _signInManager.PasswordSignInAsync(existingUser, loginViewModel.Password, false, false);

            if(!signInResult.Succeeded)
            {

                return IdentityResult.Failed(new IdentityError
                {
                    Code = "LoginFailed",
                    Description = "Something went wrong please try again"
                });
            }

            return IdentityResult.Success;


        }

        public async Task<IdentityResult> LogoutEmployee()
        {
            await _signInManager.SignOutAsync();
            return IdentityResult.Success;

        }

        public async Task<(IdentityResult result, string emailConfirmationToken)> RegisterEmployee(RegisterViewModelDto registerViewModelDto)
        {
            var existingUser = await _userManager.FindByEmailAsync(registerViewModelDto.Email);
            if (existingUser != null)
            {
                return (IdentityResult.Failed(new IdentityError { Description = "Email already exists" }), null);
            }

            var employee = _mapper.Map<Employee>(registerViewModelDto);
            employee.UserName = registerViewModelDto.Email;

            var result = await _userManager.CreateAsync(employee, registerViewModelDto.Password);

            if(result.Succeeded)
            {
                employee.CreationDate = DateTime.Now;
                await _userManager.AddToRoleAsync(employee, "default");

                var emailConfirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(employee);

                // Return both result and token
                return (result, emailConfirmationToken);
            }

            return (result, null);


        }

        public async Task<IdentityResult> ResetPassword(ResetPasswordViewModelDto resetPasswordViewModel)
        {

            var user = await _userManager.FindByEmailAsync(resetPasswordViewModel.email);

            if(user is null)
            {
                return IdentityResult.Failed();
            }

            var result = await _userManager.ResetPasswordAsync(user, resetPasswordViewModel.Token,
                resetPasswordViewModel.NewPassword);

            return result;


        }

        public async Task<IdentityResult> AddAdmin(CreateAdminViewModelDto createAdminViewModelDto)
        {

            var existingEmail = await _userManager.FindByEmailAsync(createAdminViewModelDto.Email);

            if (existingEmail != null)
            {
                return IdentityResult.Failed();
            }


            var admin = _mapper.Map<Employee>(createAdminViewModelDto);
            admin.UserName = createAdminViewModelDto.Email;
            admin.EmailConfirmed = true;

            var result = await _userManager.CreateAsync(admin, createAdminViewModelDto.Password);

            if (result.Succeeded)
            {
                admin.CreationDate = DateTime.Now;
                await _userManager.AddToRoleAsync(admin, "Administrator");
            }

            return result;



        }

        // to validate if the token is for specific purpose and has not expired
        public async Task<bool> ValidateToken(string id, string Token)
        {
            var user = await _userManager.FindByIdAsync(id);

            var isTokenValid = await _userManager.VerifyUserTokenAsync(user, _userManager.Options.Tokens.PasswordResetTokenProvider, "ResetPassword", Token);

            return isTokenValid;

        }

        public async Task<string> ForgotPassword(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var token  = await _userManager.GeneratePasswordResetTokenAsync(user);

            return token;
        }

        public async Task<string> ChangeEmail(string oldEmail, string newEmail)
        {
            var user = await _userManager.FindByEmailAsync(oldEmail);

            var token = await _userManager.GenerateChangeEmailTokenAsync(user, newEmail);

            return token;

        }

        public async Task<IdentityResult> UpdateEmail(string oldEmail, string newEmail, string token)
        {
            var currUser = await _userManager.FindByEmailAsync(oldEmail);
            var result = await _userManager.ChangeEmailAsync(currUser, newEmail, token);

            return result;
        }
    }
}
