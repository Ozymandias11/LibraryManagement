using AutoMapper;
using Library.Model.Models;
using Library.Service.Dto;
using Library.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

        public async Task<IdentityResult> RegisterEmployee(RegisterViewModelDto registerViewModelDto)
        {
            var existingUser = await _userManager.FindByEmailAsync(registerViewModelDto.Email);
            if (existingUser != null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "Email already exists" });
            }

            var employee = _mapper.Map<Employee>(registerViewModelDto);
            employee.UserName = registerViewModelDto.Email;

            var result = await _userManager.CreateAsync(employee, registerViewModelDto.Password);


            return result;
            

        }

        public async Task<IdentityResult> ResetPassword(ResetPasswordViewModelDto resetPasswordViewModel)
        {
            var user = await _userManager.FindByEmailAsync(resetPasswordViewModel.Email);

            if(user == null)
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Code = "UserNotFound",
                    Description = "User not found."
                });

            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var result = await _userManager.ResetPasswordAsync(user, token, resetPasswordViewModel.NewPassword);

            
            if(!result.Succeeded)
            {
                return IdentityResult.Failed(new IdentityError { 
                    Code = "PasswordResetFailed",
                    Description = "Something went wrong" });
            }

            return result;



        }

    }
}
