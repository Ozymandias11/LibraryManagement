using AutoMapper;
using Library.Service;
using Library.Service.Dto;
using Library.Service.Interfaces;
using LibraryManagement.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LibraryManagement.Controllers
{
    public class EmployeeManagementController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private IEmailTemplateService _emailTemplateService;
        private readonly IServiceManager _serviceManager;
        public EmployeeManagementController(IMapper mapper,
            IUserService userService,
            IEmailTemplateService emailTemplateService,
            IServiceManager serviceManager)
        {

            _userService = userService;
            _mapper = mapper;
            _emailTemplateService = emailTemplateService;
            _serviceManager = serviceManager;
        }

        [Authorize(Roles = "Administrator,SuperAdmin")]
        public async Task<IActionResult> Roles()
        {
            var pendingUsers = await _userService.GetAllPendingUsers();
            ViewData["ReturnUrl"] = Request.Path;
            var PendingUserViewModel = _mapper.Map<IEnumerable<UserForPendingViewModel>>(pendingUsers);
            return View(PendingUserViewModel);
        }

        public IActionResult AssignRoles(string Id, string returnUrl)
        {
            var assignRolesViewModel = new AssignRoleViewModel
            {
                Id = Id
            };

            ViewData["ReturnUrl"] = returnUrl;

            return View(assignRolesViewModel);

        }

        [HttpPost]
        public async Task<IActionResult> AssignRoles(AssignRoleViewModelDto assignRoleViewModelDto, 
            string returnUrl)
        {
            var result = await _userService.AssignRolesToEmployees(assignRoleViewModelDto);

            if (result.Succeeded)
            {
                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }

                var currentUser = User.FindFirst(ClaimTypes.NameIdentifier).Value;

                var userRoles = await _userService.GetUserRoles(currentUser);

                if (userRoles.Contains("Administrator"))
                {
                    return RedirectToAction("Roles");
                }
                else if(userRoles.Contains("SuperAdmin"))
                {
                    return RedirectToAction("UsersForSuperAdmin");
                }


                return RedirectToAction("Roles");
            }

            return View();

        }


        [Authorize(Roles = "Administrator,SuperAdmin")]
        public async Task<IActionResult> Users(string sortBy, string sortOrder)
        {

            ViewBag.SortOrder = sortOrder;
            ViewBag.SortBy = sortBy;

            var users = await _userService.GetAllUsers(sortBy, sortOrder);
            var userViewModel = _mapper.Map<IEnumerable<UserVeiwModel>>(users);
            return View(userViewModel);
        }
        [Authorize(Roles = "Administrator,SuperAdmin")]
        public async Task<IActionResult> Templates(string sortBy, string sortOrder)
        {

            ViewBag.SortOrder = sortOrder;  
            ViewBag.SortBy = sortBy;

            var templates = await _emailTemplateService.GetAllTemplate(sortBy,sortOrder, trackChanges: false);

            var templatesViewModel = _mapper.Map<IEnumerable<EmailTemplateViewModel>>(templates);

            return View(templatesViewModel);

        }
        [Authorize(Roles = "Administrator,SuperAdmin")]
        public async Task<IActionResult> EditTemplate(Guid id)
        {
            var template = await _emailTemplateService.GetTemplateById(id, trackChanges: false);

            var templateViewModel = _mapper.Map<EmailTemplateViewModel>(template);

            return View(templateViewModel);


        }
        [Authorize(Roles = "Administrator,SuperAdmin")]
        [HttpPost]
        public async Task<IActionResult> EditTemplate(EmailTemplateViewModel emailTemplateViewModel)
        {


            var EmailTemplateDto = _mapper.Map<EmailtemplateDto>(emailTemplateViewModel);

            await _emailTemplateService.UpdateEmailTemplate(EmailTemplateDto, trackChanges: true);


            return RedirectToAction("Templates");



        }
        [Authorize(Roles = "Administrator,SuperAdmin")]
        public IActionResult AddEmployee()
        {
            var createEmployeeViewModel = new CreateEmployeeViewModel();
            return View(createEmployeeViewModel);
        }
        [Authorize(Roles = "Administrator,SuperAdmin")]
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
        [Authorize(Roles = "Administrator,SuperAdmin")]
        public async Task<IActionResult> DeleteUser(UserVeiwModel userVeiwModel)
        {
            var userViewModelDto = _mapper.Map<UserViewModelDto>(userVeiwModel);
            var result = await _userService.DeleteUser(userViewModelDto);
            if (result.Succeeded)
            {

                var currentUser = User.FindFirst(ClaimTypes.NameIdentifier).Value;

                var userRoles = await _userService.GetUserRoles(currentUser);

                if (userRoles.Contains("Administrator"))
                {
                    return RedirectToAction("Users");

                }else if (userRoles.Contains("SuperAdmin"))
                {
                    return RedirectToAction("UsersForSuperAdmin");
                }



                return RedirectToAction("Users");
            }

            return View(userViewModelDto);
        }
        [Authorize(Roles = "Administrator,SuperAdmin")]
        public async Task<IActionResult> DeletedUsers()
        {
            var deltedeUserDtos = await _userService.GetDeletedUsers();

            var deletedUSerViewModels = _mapper.Map<IEnumerable<UserVeiwModel>>(deltedeUserDtos);

            return View(deletedUSerViewModels);


        }

        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> UsersForSuperAdmin(string sortBy, string sortOrder)
        {
            ViewBag.SortBy = sortBy;    
            ViewBag.SortOrder = sortOrder;

            var users = await _userService.GetAllUsersSuper(sortBy, sortOrder);
            var userViewModel = _mapper.Map<IEnumerable<UserVeiwModel>>(users);
            return View(userViewModel);
        }
        [Authorize(Roles = "SuperAdmin")]
        public IActionResult AddAdmin()
        {
            var creaeteAdminViewModel = new CreateAdminViewModel();
            return View(creaeteAdminViewModel);
        }

        [HttpPost]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> AddAdmin(CreateAdminViewModel createAdminViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(createAdminViewModel);
            }

            var createAdminViewModelDto = _mapper.Map<CreateAdminViewModelDto>(createAdminViewModel);

            var result = await _serviceManager.AuthenticationService.AddAdmin(createAdminViewModelDto);

            if (result.Succeeded)
            {
                return RedirectToAction("Users");
            }

            return View(createAdminViewModelDto);
        }

        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }


    }
}
