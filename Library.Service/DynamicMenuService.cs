using AutoMapper;
using Library.Data.Interfaces;
using Library.Model.Models;
using Library.Service.Dto;
using Library.Service.Interfaces;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Library.Service
{
    public class DynamicMenuService : IDynamicMenuService
    {

        private readonly IDynamicMenuRepository _repository;
        private readonly IMapper _mapper;
        private readonly UserManager<Employee> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public DynamicMenuService(IDynamicMenuRepository repository,
            IMapper mapper, UserManager<Employee> userManager, RoleManager<IdentityRole> roleManager
            )
        {
            _repository = repository;
            _mapper = mapper;   
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<List<NavigationMenuDto>> GetMenuItemsAsyncService(ClaimsPrincipal claimsPrincipal)
        {
            Console.WriteLine("Service");

            var roleIds = await GetUserRoleIdsAsync(claimsPrincipal);

            var data = await _repository.GetMenuItemsAsync(claimsPrincipal, roleIds);

            var userNamemenuItem = data.FirstOrDefault(m => m.Name == "UserName");

            if (userNamemenuItem != null)
            {
                var userName = claimsPrincipal.Identity.Name;
                userNamemenuItem.Name = userName;
            }


            
            var dataDto = _mapper.Map<List<NavigationMenuDto>>(data);

            return dataDto;

        }

        private async Task<List<string>> GetUserRoleIdsAsync(ClaimsPrincipal principal)
        {
            var user = await _userManager.GetUserAsync(principal);
            if (user == null)
            {
                return new List<string>();
            }

            var userRoles = await _userManager.GetRolesAsync(user);
            var roleIds = new List<string>();

            foreach (var roleName in userRoles)
            {
                var role = await _roleManager.FindByNameAsync(roleName);
                if (role != null)
                {
                    roleIds.Add(role.Id);
                }
            }

            return roleIds;
        }

    }
}
