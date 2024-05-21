using AutoMapper;
using Library.Model.Models;
using Library.Service.Dto;
using Library.Service.Extensions;
using Library.Service.Interfaces;
using Mailjet.Client.Resources;
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

        public async Task<IdentityResult> DeleteUser(UserViewModelDto user)
        {
            var id = user.Id;
            var employee = await _userManager.FindByIdAsync(id);

            employee.DeleteDate = DateTime.Now;
            await _userManager.UpdateAsync(employee);

            return IdentityResult.Success;
            
        }

        public async Task<IEnumerable<UserForPendingViewModelDto>> GetAllPendingUsers()
        {
            var usersWithDefaultRole = await _userManager.GetUsersInRoleAsync("default");
            var usersWithDefaultRoleDto = _mapper.Map<IEnumerable<UserForPendingViewModelDto>>(usersWithDefaultRole);
                

            return usersWithDefaultRoleDto;


        }

        public async Task<IEnumerable<UserViewModelDto>> GetAllUsersSuper()
        {
            var users = await _userManager.Users.ToListAsync();

            var userViewModelDto = users.Select(user =>
            {
                var roles = _userManager.GetRolesAsync(user).Result;
                var userViewModelDto = _mapper.Map<UserViewModelDto>(user);
                userViewModelDto.Roles = string.Join(", ", roles);
                return userViewModelDto;

            }
            );

            return userViewModelDto;


        }
        //public async Task<IEnumerable<UserViewModelDto>> GetAllUsers()
        //{
        //    var users = await _userManager.Users
        //        .Where(u => u.DeleteDate == null)
        //        .ToListAsync();

        //    var filteredUsers = users.Where(user =>
        //        !_userManager.IsInRoleAsync(user, "SuperAdmin").Result &&
        //        !_userManager.IsInRoleAsync(user, "Administrator").Result && 
        //        !_userManager.IsInRoleAsync(user, "Default").Result);

        //    var userViewModelsDto = filteredUsers.Select(user =>
        //    {
        //        var roles = _userManager.GetRolesAsync(user).Result;
        //        var userViewModel = _mapper.Map<UserViewModelDto>(user);
        //        userViewModel.Roles = string.Join(", ", roles);
        //        return userViewModel;
        //    });

        //    return userViewModelsDto;

        //}
        public async Task<IEnumerable<UserViewModelDto>> GetAllUsers(string sortBy, string sortOrder)
        {
            var users = await _userManager.Users
                .Where(u => u.DeleteDate == null)
                .ToListAsync();

            var filteredUsers = users.Where(user =>
                !_userManager.IsInRoleAsync(user, "SuperAdmin").Result &&
                !_userManager.IsInRoleAsync(user, "Administrator").Result &&
                !_userManager.IsInRoleAsync(user, "Default").Result);


            switch (sortBy)
            {
                case "Email":
                    filteredUsers = sortOrder == "Email_Asc"
                        ? filteredUsers.OrderBy(u => u.Email)
                        : filteredUsers.OrderByDescending(u => u.Email);
                    break;
                case "RegistrationDate":
                    filteredUsers = sortOrder == "RegistrationDate_Asc"
                        ? filteredUsers.OrderBy(u => u.CreationDate)
                        : filteredUsers.OrderByDescending(u => u.CreationDate);
                    break;
                default:
                    // Default sorting if no valid sortBy parameter is provided
                    filteredUsers = filteredUsers.OrderBy(u => u.Email);
                    break;
            }

            var userViewModelsDto = filteredUsers.Select(user =>
            {
                var roles = _userManager.GetRolesAsync(user).Result;
                var userViewModel = _mapper.Map<UserViewModelDto>(user);
                userViewModel.Roles = string.Join(", ", roles);
                return userViewModel;
            });

            return userViewModelsDto;

        }

        public async Task<IEnumerable<UserViewModelDto>> GetDeletedUsers()
        {
            var users = await _userManager.Users.Where(u => u.DeleteDate != null).ToListAsync();

            var usersDto = users.Select(user =>
            {
                var roles = _userManager.GetRolesAsync(user).Result;
                var userViewModel = _mapper.Map<UserViewModelDto>(user);
                userViewModel.Roles = string.Join (", ", roles);
                return userViewModel;
            });

            return usersDto;


        }


        public async Task<IEnumerable<UserViewModelDto>> GetUsersAsync(string propertyName, string sortOrder)
        {
            if (string.IsNullOrWhiteSpace(propertyName))
            {
                propertyName = "Email";
                sortOrder = "ASC";
            }
            var users = await _userManager.Users.Sort(propertyName, sortOrder).ToListAsync();

            var usersDto = users.Select(user =>
            {
                var roles = _userManager.GetRolesAsync(user).Result;
                var userViewModel = _mapper.Map<UserViewModelDto>(user);
                userViewModel.Roles = string.Join(", ", roles);
                return userViewModel;
            });

            return usersDto;


        }

        public async Task<UserViewModelDto> GetUserById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            var roles = _userManager.GetRolesAsync(user).Result;

            var userDto = _mapper.Map<UserViewModelDto>(user);  
            userDto.Roles = string.Join(", ", roles);
            return userDto;


        }
    }
}
