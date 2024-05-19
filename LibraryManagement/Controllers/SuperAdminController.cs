using AutoMapper;
using Library.Service.Interfaces;
using LibraryManagement.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    public class SuperAdminController : Controller
    {
        private readonly ISuperAdminUserService _superAdminUserService;
        private readonly IMapper _mapper;
        public SuperAdminController(ISuperAdminUserService superAdminUserService, IMapper mapper)
        {
            _superAdminUserService = superAdminUserService;
            _mapper = mapper;
        }
        public async Task<IActionResult> Users()
        {
            var users = await _superAdminUserService.GetAllUsers();
            var userViewModel = _mapper.Map<IEnumerable<UserVeiwModel>>(users);
            return View(userViewModel);
        }
    }
}
