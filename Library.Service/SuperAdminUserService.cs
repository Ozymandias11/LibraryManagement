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
    public class SuperAdminUserService : ISuperAdminUserService
    {

        private readonly UserManager<Employee> _userManager;
        private readonly IMapper _mapper;

        public SuperAdminUserService(UserManager<Employee> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserViewModelDto>> GetAllUsers()
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

    }
}
