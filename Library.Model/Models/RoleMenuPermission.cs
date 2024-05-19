using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model.Models
{
   
    public class RoleMenuPermission
    {
        
        public Guid Id { get; set; }

      
        public string RoleId { get; set; }

       
        public Guid NavigationMenuId { get; set; }

        public NavigationMenu NavigationMenu { get; set; }

       
    }
}
