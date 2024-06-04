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
using System.Globalization;
using System.Linq;
using System.Security.Claims;
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

            var currentRoles = await _userManager.GetRolesAsync(user);

            await _userManager.RemoveFromRolesAsync(user, currentRoles);

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


        //კოდის დუპლიკაციის მოსაგვარებლად quryBuidler-ы ან რამე მსგავს generic მეთოდს შევქმნი
        
        public async Task<IEnumerable<UserViewModelDto>> GetAllUsersSuper(string sortBy, string sortOrder)
        {
            var users = await _userManager.Users.ToListAsync();

            var userViewModelDtos = new List<UserViewModelDto>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                var userViewModelDto = _mapper.Map<UserViewModelDto>(user);
                userViewModelDto.Roles = string.Join(", ", roles);
                userViewModelDtos.Add(userViewModelDto);
            }

            IEnumerable<UserViewModelDto> sortedUsers = sortBy switch
            {
                "Email" => sortOrder == "Email_Asc"
                                        ? userViewModelDtos.OrderBy(u => u.Email)
                                        : userViewModelDtos.OrderByDescending(u => u.Email),
                "RegistrationDate" => sortOrder == "RegistrationDate_Asc"
                                        ? userViewModelDtos.OrderBy(u => u.CreationDate)
                                        : userViewModelDtos.OrderByDescending(u => u.CreationDate),
                "Roles" => sortOrder == "Roles_Asc"
                                        ? userViewModelDtos.OrderBy(u => u.Roles)
                                        : userViewModelDtos.OrderByDescending(u => u.Roles),
                _ => userViewModelDtos.OrderBy(u => u.Email),// Default sorting if no valid sortBy parameter is provided
            };
            return sortedUsers;
        }
    


        public async Task<IEnumerable<UserViewModelDto>> GetAllUsers(string sortBy, string sortOrder, string searchString)
        {
            var users = await _userManager.Users
                .Where(u => u.DeleteDate == null)
                .ToListAsync();

            var filteredUsers = users.Where(user =>
                !_userManager.IsInRoleAsync(user, "SuperAdmin").Result &&
                !_userManager.IsInRoleAsync(user, "Default").Result);


            if (!string.IsNullOrEmpty(searchString))
            {
                filteredUsers = filteredUsers.Where(user => user.Email.Contains(searchString) 
                || user.PhoneNumber.Contains(searchString));
            }


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

        public async Task<IEnumerable<UserViewModelDto>> GetDeletedUsers(string sortBy, string sortOrder, string searchString)
        {
            IEnumerable<Employee> users = await _userManager.Users.Where(u => u.DeleteDate != null).ToListAsync();


            if(!string.IsNullOrEmpty(searchString))
            {
                users = users.Where(user => user.Email.Contains(searchString)
                || user.PhoneNumber.Contains(searchString));
            }

            switch (sortBy)
            {
                case "Email":
                    users = sortOrder == "Email_Asc"
                        ? users.OrderBy(u => u.Email)
                        : users.OrderByDescending(u => u.Email);
                    break;
                case "RegistrationDate":
                    users = sortOrder == "RegistrationDate_Asc"
                        ? users.OrderBy(u => u.CreationDate)
                        : users.OrderByDescending(u => u.CreationDate);
                    break;
                default:
                    // Default sorting if no valid sortBy parameter is provided
                    users = users.OrderBy(u => u.Email);
                    break;
            }



            var usersDto = users.Select(user =>
            {
                var roles = _userManager.GetRolesAsync(user).Result;
                var userViewModel = _mapper.Map<UserViewModelDto>(user);
                userViewModel.Roles = string.Join (", ", roles);
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

        public async Task<UserViewModelDto> GetUserByEmail(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var userDto = _mapper.Map<UserViewModelDto>(user);

            return userDto;
        }

        public async Task<IList<string>> GetUserRoles(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            return await _userManager.GetRolesAsync(user);
        }

        public async Task<UserViewModelDto> GetUserWithClaimsPrincipal(ClaimsPrincipal claimsPrincipal)
        {
            var user = await _userManager.GetUserAsync(claimsPrincipal);

            var roles =  _userManager.GetRolesAsync(user).Result;

            var userDto = _mapper.Map<UserViewModelDto>(user);

            userDto.Roles = string.Join(", ", roles);

            return userDto;
        }

        public async Task<bool> CheckIfEmailExists(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return user != null;
        }

        public async Task UpdateProfile(UserViewModelProfileDto userViewModelProfileDto, bool trackChanges)
        {
            var curruser = await _userManager.FindByEmailAsync(userViewModelProfileDto.Email);


            _mapper.Map(userViewModelProfileDto, curruser);
            curruser.UpdateDate = DateTime.Now;

            await _userManager.UpdateAsync(curruser);

        }

        public async Task<IdentityResult> RenewEmployee(UserViewModelDto userViewModelDto)
        {
           var employee = await _userManager.FindByIdAsync(userViewModelDto.Id);

            employee.DeleteDate = null;
            await _userManager.UpdateAsync(employee);

            return IdentityResult.Success;


        }

    }
}
