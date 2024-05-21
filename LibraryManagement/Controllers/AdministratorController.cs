﻿using AutoMapper;
using Library.Service.Dto;
using Library.Service.Interfaces;
using LibraryManagement.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Globalization;


namespace LibraryManagement.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdministratorController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private IEmailTemplateService _emailTemplateService;

        public AdministratorController(IMapper mapper,
            IUserService userService,
            IEmailTemplateService emailTemplateService)
        {

            _userService = userService;
            _mapper = mapper;
            _emailTemplateService = emailTemplateService;
        }

        public async Task<IActionResult> Roles()
        {
           var pendingUsers = await _userService.GetAllPendingUsers();
  
           var PendingUserViewModel = _mapper.Map<IEnumerable<UserForPendingViewModel>>(pendingUsers);
           return View(PendingUserViewModel);
        }

        public IActionResult AssignRoles(string Id)
        {
            var assignRolesViewModel = new AssignRoleViewModel
            {
                Id = Id
            };

            return View(assignRolesViewModel);

        }
        [HttpPost]
        public async Task<IActionResult> AssignRoles(AssignRoleViewModelDto assignRoleViewModelDto)
        {
            var result = await _userService.AssignRolesToEmployees(assignRoleViewModelDto);

            if (result.Succeeded)
            {
                return RedirectToAction("Roles");
            }

            return View();

        }


        public async Task<IActionResult> Users(string sortBy, string sortOrder)
        {

            ViewBag.SortOrder = sortOrder;
            ViewBag.SortBy = sortBy;

            var users = await _userService.GetAllUsers(sortBy, sortOrder);
            var userViewModel = _mapper.Map<IEnumerable<UserVeiwModel>>(users);
            return View(userViewModel);
        }

        public async Task<IActionResult> Templates()
        {
            var templates = await _emailTemplateService.GetAllTemplate(trackChanges: false);

            var templatesViewModel = _mapper.Map<IEnumerable<EmailTemplateViewModel>>(templates);

            return View(templatesViewModel);

        }

        public async Task<IActionResult> EditTemplate(Guid id)
        {
            var template = await _emailTemplateService.GetTemplateById(id, trackChanges:false);

            var templateViewModel = _mapper.Map<EmailTemplateViewModel>(template);

            return View(templateViewModel);


        }

        [HttpPost]
        public async Task<IActionResult> EditTemplate(EmailTemplateViewModel emailTemplateViewModel)
        {


            var EmailTemplateDto = _mapper.Map<EmailtemplateDto>(emailTemplateViewModel);

            await _emailTemplateService.UpdateEmailTemplate(EmailTemplateDto, trackChanges:true);


            return RedirectToAction("Templates");



        }

        public IActionResult AddEmployee()
        {
            var createEmployeeViewModel = new CreateEmployeeViewModel();
            return View(createEmployeeViewModel);
        }

        public IActionResult DeleteUser(string id, string email)
        {

            var userViewModel = new UserVeiwModel
            {
                Id = id,
                Email = email
            };


            return View(userViewModel);

        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(UserVeiwModel userVeiwModel)
        {
            var userViewModelDto = _mapper.Map<UserViewModelDto>(userVeiwModel);
            var result = await _userService.DeleteUser(userViewModelDto);
            if(result.Succeeded)
            {
                return RedirectToAction("Users");
            }

            return View(userViewModelDto);
        }

        public async Task<IActionResult> DeletedUsers()
        {
            var deltedeUserDtos = await _userService.GetDeletedUsers();

            var deletedUSerViewModels = _mapper.Map<IEnumerable<UserVeiwModel>>(deltedeUserDtos);

            return View(deletedUSerViewModels);


        }






    }
}
