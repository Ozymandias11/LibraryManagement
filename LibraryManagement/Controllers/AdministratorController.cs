using AutoMapper;
using Library.Model.Models;
using Library.Service.Interfaces;
using LibraryManagement.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers
{
    public class AdministratorController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public AdministratorController(IMapper mapper,
            IUserService userService)
        {
            
            _userService = userService; 
            _mapper = mapper;
        }

        public async Task<IActionResult> Roles()
        {
            return View();
        }

        public async Task<IActionResult> Users()
        {
            var users =  await _userService.GetAllUsers();
            var userViewModel = _mapper.Map<IEnumerable<UserVeiwModel>>(users);
            return View(userViewModel); 
        }
    }

}