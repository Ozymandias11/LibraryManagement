using AutoMapper;
using Library.Service.Dto;
using Library.Service.Interfaces;
using LibraryManagement.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    public class SuperAdminController : Controller
    {
        private readonly IUserService _userService;
        private readonly IServiceManager _serviceManager;
        private readonly IMapper _mapper;
        public SuperAdminController(IUserService userService,
            IMapper mapper,
            IServiceManager serviceManager)
        {
            _userService = userService;
            _mapper = mapper;
            _serviceManager = serviceManager;

        }
        public async Task<IActionResult> Users()
        {
            var users = await _userService.GetAllUsersSuper();
            var userViewModel = _mapper.Map<IEnumerable<UserVeiwModel>>(users);
            return View(userViewModel);
        }
        public IActionResult AddAdmin()
        {
            var creaeteAdminViewModel = new CreateAdminViewModel();
            return View(creaeteAdminViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> AddAdmin(CreateAdminViewModel createAdminViewModel)
        {
            if(!ModelState.IsValid)
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

    }
}
