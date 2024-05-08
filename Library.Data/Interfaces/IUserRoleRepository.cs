using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data.Interfaces
{
    public interface IUserRoleRepository
    {
        Task<List<string>> GetUserRoleIds(ClaimsPrincipal ctx);
    }
}
