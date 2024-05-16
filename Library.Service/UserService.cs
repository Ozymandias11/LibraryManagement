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

        public async Task<IdentityResult> AssignRolesToEmployees(AssignRoleViewModelDto assignRoleViewModelDto)
        {
            var user = await _userManager.FindByIdAsync(assignRoleViewModelDto.Id);

            await _userManager.RemoveFromRoleAsync(user, "Default");

            var result = await _userManager.AddToRolesAsync(user, assignRoleViewModelDto.SelectedRoles);

            return result;


        }

        public async Task<IdentityResult> CreateEmployee(CreateEmployeeViewModelDto createEmployeeViewModelDto)
        {
            var employee = new Employee
            {
                UserName = createEmployeeViewModelDto.Email,
                FirstName = createEmployeeViewModelDto.FirstName,
                LastName = createEmployeeViewModelDto.LastName,
                Email = createEmployeeViewModelDto.Email,
                EmailConfirmed = false
                
            };

            var result = await _userManager.CreateAsync(employee);

            if(result.Succeeded)
            {
                if(createEmployeeViewModelDto.SelectedRoles 
                    != null && createEmployeeViewModelDto.SelectedRoles.Count != 0)
                {
                    await _userManager.AddToRolesAsync(employee, createEmployeeViewModelDto.SelectedRoles);
                }
                
            }

            return result;


        }

        public async Task<IEnumerable<UserForPendingViewModelDto>> GetAllPendingUsers()
        {
            var usersWithDefaultRole = await _userManager.GetUsersInRoleAsync("default");
            var usersWithDefaultRoleDto = _mapper.Map<IEnumerable<UserForPendingViewModelDto>>(usersWithDefaultRole);

            return usersWithDefaultRoleDto;


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
