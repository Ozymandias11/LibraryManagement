﻿using Library.Model.Models;
using Library.Service.Dto;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Library.Service.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserViewModelDto>> GetAllUsers(string sortBy, string sortOrder);
        Task<IEnumerable<UserForPendingViewModelDto>> GetAllPendingUsers();
        Task<IdentityResult> AssignRolesToEmployees(AssignRoleViewModelDto assignRoleViewModelDto);
        Task<UserViewModelDto> GetUserById(string id);
        Task<IdentityResult> DeleteUser(UserViewModelDto user);
        Task<IEnumerable<UserViewModelDto>> GetAllUsersSuper(string sortBy, string sortOrder);
        Task<IEnumerable<UserViewModelDto>> GetDeletedUsers();
        Task<IList<string>> GetUserRoles(string id);
        Task<UserViewModelDto> GetUserWithClaimsPrincipal(ClaimsPrincipal claimsPrincipal);
        Task<bool> CheckIfEmailExists(string email);
        Task UpdateProfile(UserViewModelProfileDto userViewModelProfileDto, bool trackChanges);
      //  Task UpdateEmail(string email);
        Task<UserViewModelDto> GetUserByEmail(string email);
    }
}

