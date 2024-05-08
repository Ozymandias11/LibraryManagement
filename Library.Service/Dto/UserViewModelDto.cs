using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Service.Dto
{
   public record UserViewModelDto
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }
        public string Roles { get; set; }
    }
}
