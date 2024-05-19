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
        private readonly IMapper _mapper;

        public UserService(UserManager<Employee> userManager,
            IMapper mapper)
        {
            _userManager = userManager; 
            _mapper = mapper;
        }

        public async Task<IdentityResult> AssignRolesToEmployees(AssignRoleViewModelDto assignRoleViewModelDto)
        {
            var user = await _userManager.FindByIdAsync(assignRoleViewModelDto.Id);

            await _userManager.RemoveFromRoleAsync(user, "Default");

            var result = await _userManager.AddToRolesAsync(user, assignRoleViewModelDto.SelectedRoles);

            if(result.Succeeded)
            {
                user.UpdateDate = DateTime.Now;
                await _userManager.UpdateAsync(user);
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
            var users = await _userManager.Users
                .Where(u => u.DeleteDate == null)
                .ToListAsync();

            var filteredUsers = users.Where(user =>
                !_userManager.IsInRoleAsync(user, "SuperAdmin").Result &&
                !_userManager.IsInRoleAsync(user, "Administrator").Result && 
                !_userManager.IsInRoleAsync(user, "Default").Result);

            var userViewModelsDto = filteredUsers.Select(user =>
            {
                var roles = _userManager.GetRolesAsync(user).Result;
                var userViewModel = _mapper.Map<UserViewModelDto>(user);
                userViewModel.Roles = string.Join(", ", roles);
                return userViewModel;
            });

            return userViewModelsDto;

        }

        
    }
}
