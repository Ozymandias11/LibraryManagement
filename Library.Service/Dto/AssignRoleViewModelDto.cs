﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Service.Dto
{
   public record AssignRoleViewModelDto(string Id, ICollection<string> SelectedRoles);
}
