using Library.Service.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Library.Service.Interfaces
{
   public interface IDynamicMenuService
    {
        Task<List<NavigationMenuDto>> GetMenuItemsAsyncService(ClaimsPrincipal claimsPrincipal);
    }
}
