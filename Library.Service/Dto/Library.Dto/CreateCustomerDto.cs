using Library.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Service.Dto.Library.Dto
{
   public record CreateCustomerDto(
       string FirstName,
       string LastName,
       string Email,
       string PhoneNumber,
       Address Address,
       string CustomerPersonalId);
   
}
