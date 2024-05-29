using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Service.Dto
{
    public record UserViewModelProfileDto(
        string FirstName ,
        string LastName ,
        string Email,
        string PhoneNumber,
        string Roles
        
        );


}
