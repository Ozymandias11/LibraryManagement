﻿using Library.Data.Interfaces;
using Library.Model.Models;
using Microsoft.AspNetCore.Identity;
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
        public DynamicMenuRepository(RepositoryContext repositoryContext
            ) : base(repositoryContext)
        {
   
        }

        public async Task<List<NavigationMenu>> GetMenuItemsAsync(ClaimsPrincipal principal, List<string> roleIds)
        {

            Console.WriteLine("Repository");

            var isAuthenticated = principal.Identity.IsAuthenticated;

            if (!isAuthenticated)
            {
                return new List<NavigationMenu>();
            }

 

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
