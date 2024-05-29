using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using Library.Model.Models;
using Library.Service.Dto;
using Library.Service.Interfaces;
using LibraryManagement.Extensions;
using LibraryManagement.ViewModels;
using Mailjet.Client.Resources;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Web.Helpers;


namespace LibraryManagement.Controllers
{
    public class AccountController : Controller
    {

        private readonly IServiceManager _serviceManager;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly IUserService _userService;
     
        public AccountController(IServiceManager serviceManager,
            IMapper mapper,
            IEmailService emailService,
            IUserService userService)    
        {
            _serviceManager = serviceManager;
            _mapper = mapper;
            _emailService = emailService;   
            _userService = userService;
           
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
                var emailSent = await _emailService.SendEmail(registerViewModelDto, "Email Verification");

                if (emailSent)
                {
                    return RedirectToAction("CheckEmail");
                }

            }
            else
            {
                registerViewModel.ErrorMessage = "Email already exists";
            }



            return View(registerViewModel);


        }

        public async Task<IActionResult> ConfirmEmail(string token ,string userId)
        {
            var result = await _serviceManager.AuthenticationService.ConfirmEmail(token, userId);

            if (result.Succeeded)
            {
                return RedirectToAction("RegistrationCompleted");
            }

            return View();

        }

        public IActionResult RegistrationCompleted()
        {
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

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                loginViewModel.ErrorMessage = "Wrong credentials, please try again.";
            }

            return View(loginViewModel);


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
            else
            {
                forgotPasswordViewModel.ErrorMessage = "Wrong Email";
            }
            return View(forgotPasswordViewModel);

        }

        public IActionResult CheckEmail()
        {

            return View();
        }

        public async Task<IActionResult> ResetPassword(string token, string userId)
        {

            var isTokenValid = await _serviceManager.AuthenticationService.ValidateToken(userId, token);

            ViewData["ShowNavbar"] = false;

            if (!isTokenValid)
            {
                return View("TokenExpired");
            }


            ViewData["IsPasswordResetLink"] = true;
            ViewData["ShowNavbar"] = false;

            if (token == null)
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
                ViewData["ShowNavbar"] = false;
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

        // for admins and superAdmins to view

        public async Task<IActionResult> Profile(string id)
        {


            var user = await _userService.GetUserById(id);
            var userViewModel = new UserVeiwModel() 
            {
                Id = user.Id,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Roles = string.Join(", ", user.Roles),
                EmailConfirmed = user.EmailConfirmed,
                CreationDate = user.CreationDate,
                DeleteDate = user.DeleteDate
            };


            ViewData["ReturnUrl"] = Url.Action("Profile", new { id = id });
            ViewData["cancelUrl"] = Url.Action("Profile", "Account", new {id = id});
            return View(userViewModel);




        
        }


        //for actual user profiles, more control
        public async Task<IActionResult> UserProfile()
        {
            var user = await _userService.GetUserWithClaimsPrincipal(User);

            var userViewModel = new UserViewModelProfile()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Roles = string.Join(", ", user.Roles),
            };

          

            return View(userViewModel);


        }

        [HttpPost]
        public async Task<IActionResult> UserProfile(UserViewModelProfile userViewModelProfile)
        {
            if (!ModelState.IsValid)
            {
                return View(userViewModelProfile);
            }

            var currentUserEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

            var existingEmail = await _userService.CheckIfEmailExists(userViewModelProfile.Email);

            if (existingEmail && userViewModelProfile.Email != currentUserEmail)
            {
                userViewModelProfile.Email = currentUserEmail;
                userViewModelProfile.ErrorMessage = "The email already exists";
                return View(userViewModelProfile);
            }

            //tracking if the user wants to change email

            if(currentUserEmail != userViewModelProfile.Email)
            {
                var emailSent = await _emailService.SendEmail(userViewModelProfile, "Email Verification");
                if (emailSent)
                {
                    return RedirectToAction("CheckEmail");
                }

            }

            

            return View(userViewModelProfile);
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
