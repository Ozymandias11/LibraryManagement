using Library.Model.Models;
using Library.Service.Dto;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Service.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserViewModelDto>> GetAllUsers();
        Task<IdentityResult> CreateEmployee(CreateEmployeeViewModelDto createEmployeeViewModelDto);
        Task<IEnumerable<UserForPendingViewModelDto>> GetAllPendingUsers();
        Task<IdentityResult> AssignRolesToEmployees(AssignRoleViewModelDto assignRoleViewModelDto);
    }
}
