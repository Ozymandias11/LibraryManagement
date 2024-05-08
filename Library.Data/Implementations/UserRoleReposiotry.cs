using Library.Data.Interfaces;
using Library.Data.NewFolder;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Library.Data.Implementations
{
    public class UserRoleReposiotry : RepositoryBase<IdentityUserRole<string>>, IUserRoleRepository
    {
        public UserRoleReposiotry(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<List<string>> GetUserRoleIds(ClaimsPrincipal ctx)
        {
            var userId = GetUserId(ctx);
            var data = await FindByCondition(role => role.UserId == userId, trackChanges:false)
                .Select(role => role.RoleId)  
                .ToListAsync();

            return data;
        }

        private string GetUserId(ClaimsPrincipal user)
        {
            return ((ClaimsIdentity)user.Identity).FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
    }
}
