using Library.Service.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Service.Interfaces
{
   public interface ISuperAdminUserService
    {
        Task<IEnumerable<UserViewModelDto>> GetAllUsers();



    }
}
