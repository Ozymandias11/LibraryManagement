using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Service.Dto
{
    public record UserForPendingViewModelDto(
        string Id,
        string Email, 
        string PhoneNumber,
        bool EmailConfirmed,
        DateTime CreationDate );
}
