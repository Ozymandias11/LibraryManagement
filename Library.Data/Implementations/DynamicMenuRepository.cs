using Library.Data.Interfaces;
using Library.Model.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data.Implementations
{
    public class DynamicMenuRepository : RepositoryBase<RoleMenuPermission>, IDynamicMenuRepository
    {
        private readonly IUserRoleRepository _userRoleRepository;
        public DynamicMenuRepository(RepositoryContext repositoryContext, IUserRoleRepository userRoleRepository) : base(repositoryContext)
        {
            _userRoleRepository = userRoleRepository;
        }

        public async Task<List<NavigationMenu>> GetMenuItemsAsync(ClaimsPrincipal principal)
        {

            Console.WriteLine("Repository");

            var isAuthenticated = principal.Identity.IsAuthenticated;

            if (!isAuthenticated)
            {
                return new List<NavigationMenu>();
            }

            var roleIds = await _userRoleRepository.GetUserRoleIds(principal);

            var data = await FindByCondition(menu => roleIds.Contains(menu.RoleId), trackChanges: false).
                Select(m => new NavigationMenu
                {
                    Id = m.NavigationMenu.Id,
                    Name = m.NavigationMenu.Name,
                    ActionName = m.NavigationMenu.ActionName,
                    ControllerName = m.NavigationMenu.ControllerName,
                    ParentMenuId = m.NavigationMenu.ParentMenuId
                })
                .Distinct()
                .ToListAsync();

            return data;


        }

      

      

    }
}
