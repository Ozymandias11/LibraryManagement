using FluentValidation;
using FluentValidation.AspNetCore;
using Library.Model.Models;
using Library.Service.Interfaces;
using LibraryManagement.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;
using Shared.ViewModels;

namespace LibraryManagement.Controllers
{
    public class AccountController : Controller
    {

        private readonly IServiceManager _serviceManager;
     
        public AccountController(IServiceManager serviceManager)    
        {
            _serviceManager = serviceManager;
           
        }
        
            
        public IActionResult Register()
        {
           var response = new RegisterViewModel();
           return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(registerViewModel);
            }

            var result = await _serviceManager.AuthenticationService.RegisterEmployee(registerViewModel);
            return HandleResult(result, registerViewModel, "Registration Completed Sucesfully");


        }

        public IActionResult Login()
        {
            var response = new LoginViewModel();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(loginViewModel);
            }

            var result = await _serviceManager.AuthenticationService.LoginEmployee(loginViewModel);
            return HandleResult(result, loginViewModel, "Login Completed Succesfully");

        }

        public async Task<IActionResult> Logout()
        {
            await _serviceManager.AuthenticationService.LogoutEmployee();
            TempData["LogoutMessage"] = "You have been logged out";
            return RedirectToAction("Index", "Home");

        }

        public IActionResult ResetPassword()
        {
            var resetPasswordViewModel = new ResetPasswordViewModel();
            return View(resetPasswordViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel resetPasswordViewModel)
        {

         
            if (!ModelState.IsValid)
            {
                return View(resetPasswordViewModel);
            }

            var result = await _serviceManager.AuthenticationService.ResetPassword(resetPasswordViewModel);
            return HandleResult(result, resetPasswordViewModel, "Password reset completed sucesfully");



        }




        // helper method to store and provide user-friendly messages to UI
        private IActionResult HandleResult(IdentityResult result, object viewModel, string successMessage)
        {
            if (result.Succeeded)
            {
                TempData["SuccessMessage"] = successMessage;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                var errorMessages = new List<string>();
                foreach (var error in result.Errors)
                {
                    errorMessages.Add(ErrorHelper.GetErrorMessageForCode(error.Code));
                }
                TempData["ErrorMessages"] = errorMessages;
            }

            return View(viewModel);
        }

        



    }
}
