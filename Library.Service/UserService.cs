using AutoMapper;
using Library.Model.Models;
using Library.Service.Dto;
using Library.Service.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Service
{
    public class UserService : IUserService
    {

        private readonly UserManager<Employee> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public UserService(UserManager<Employee> userManager,
            IMapper mapper,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager; 
            _mapper = mapper;
            _roleManager = roleManager;
        }

        public async Task<IEnumerable<UserViewModelDto>> GetAllUsers()
        {
            var users = await _userManager.Users.ToListAsync();

           
            var userViewModelsDto = new List<UserViewModelDto>();   
            
            foreach(var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);

                var userViewModel = _mapper.Map<UserViewModelDto>(user);

                userViewModel.Roles = string.Join(", ", roles);

                userViewModelsDto.Add(userViewModel);
                

            }

            return userViewModelsDto;

        }
    }
}
