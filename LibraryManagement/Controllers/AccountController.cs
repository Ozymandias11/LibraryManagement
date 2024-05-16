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
        private readonly IEmailService _emailService;
     
        public AccountController(IServiceManager serviceManager,
            IMapper mapper,
            IEmailService emailService)    
        {
            _serviceManager = serviceManager;
            _mapper = mapper;
            _emailService = emailService;   
           
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

            if (result.Succeeded)
            {
                var emailSent = await _emailService.SendConfirmationEmail(registerViewModelDto, "Email Verification");

                if (emailSent)
                {
                    return RedirectToAction("CheckEmail");
                }

            }


            return HandleResult(result, registerViewModel, "Registration Completed Sucesfully");


        }

        public async Task<IActionResult> ConfirmEmail(string token ,string userId)
        {
            var result = await _serviceManager.AuthenticationService.ConfirmEmail(token, userId);

            if (result.Succeeded)
            {
                return RedirectToAction("Login");
            }

            return View();

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

     
        public IActionResult ForgotPassword()
        {
            var forgotPasswordViewModel = new ForgotPasswordViewModel();
            return View(forgotPasswordViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel forgotPasswordViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(forgotPasswordViewModel);
            }

            var forgotPasswordDto = _mapper.Map<ForgotPasswordDto>(forgotPasswordViewModel);

            bool passwordReset = await _emailService.SendEmail(forgotPasswordDto, "Password Reset");

            if (passwordReset)
            {
                return RedirectToAction("CheckEmail");
            }
            return View();

        }

        public IActionResult CheckEmail()
        {
            return View();
        }

        public IActionResult ResetPassword(string token, string userId)
        {
            if(token == null)
            {
                return View();
            }

            var resetPasswordViewModel = new ResetPasswordViewModel()
            {
                Token = token,
                UserId = userId
               
            };

            return View(resetPasswordViewModel);

        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel resetPasswordViewModel)
        {
            if(!ModelState.IsValid)
            {
                return View(resetPasswordViewModel);
            }

            var resetPasswordViewModelDto = _mapper.Map<ResetPasswordViewModelDto>(resetPasswordViewModel);


            var result = await _serviceManager.AuthenticationService.ResetPassword(resetPasswordViewModelDto);


            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation");
            }

            return View();



        }
        
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
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
