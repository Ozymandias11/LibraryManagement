﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Service.Dto
{
    public record CreateAdminViewModelDto(
     string FirstName,
     string LastName,
     string Email,
     string PhoneNumber,
     string Password,
     DateTime DateOfBirth
 );
}