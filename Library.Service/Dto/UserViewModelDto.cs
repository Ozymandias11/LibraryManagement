﻿using System;
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
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Roles { get; set; }
        public bool EmailConfirmed { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? DeleteDate { get; set; }
    }
}
