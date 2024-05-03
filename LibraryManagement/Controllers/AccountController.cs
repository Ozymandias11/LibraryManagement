using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using Library.Model.Models;
using Library.Service.Dto;
using Library.Service.Interfaces;
using LibraryManagement.Extensions;
using LibraryManagement.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;


namespace LibraryManagement.Controllers
{
    public class AccountController : Controller
    {

        private readonly IServiceManager _serviceManager;
        private readonly IMapper _mapper;
     
        public AccountController(IServiceManager serviceManager, IMapper mapper)    
        {
            _serviceManager = serviceManager;
            _mapper = mapper;
           
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


            var registerViewModelDto = _mapper.Map<RegisterViewModelDto>(registerViewModel);

            var result = await _serviceManager.AuthenticationService.RegisterEmployee(registerViewModelDto);

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

            var loginViewModelDto = _mapper.Map<LoginViewModelDto>(loginViewModel); 

            var result = await _serviceManager.AuthenticationService.LoginEmployee(loginViewModelDto);

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

            var resetPasswordViewModelDto = _mapper.Map<ResetPasswordViewModelDto>(resetPasswordViewModel);

            var result = await _serviceManager.AuthenticationService.ResetPassword(resetPasswordViewModelDto);

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
