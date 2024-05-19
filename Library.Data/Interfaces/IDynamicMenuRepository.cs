using Library.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data.Interfaces
{
    public interface IDynamicMenuRepository
    {
        Task<List<NavigationMenu>> GetMenuItemsAsync(ClaimsPrincipal principal,List<string> roleIds);
    }
}
