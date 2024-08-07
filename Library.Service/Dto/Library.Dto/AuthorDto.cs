﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Service.Dto.Library.Dto
{
   public record AuthorDto(
       Guid AuthorId,
       string FirstName,
       string LastName,
       DateTime DateOfBirth
       );
}
