using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model.Models
{

    public class NavigationMenu : BaseModel
    {

        

        public Guid Id { get; set; }

        public string? Name { get; set; }

       
        public Guid? ParentMenuId { get; set; }

        public virtual NavigationMenu? ParentNavigationMenu { get; set; }

        public string? ControllerName { get; set; }

        public string? ActionName { get; set; }

     
        public bool Permitted { get; set; }

      


    }
}
