using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Service.Dto
{
   public record CreateEmployeeViewModelDto(
       string FirstName, 
       string LastName, 
       string Email,
       string PhoneNumber,
       ICollection<string> SelectedRoles);
}
