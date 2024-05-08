using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Service.Dto
{
    public record NavigationMenuDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Guid? ParentMenuId { get; set; }
        public string ControllerName { get; set; } = string.Empty;
        public string ActionName { get; set; } = string.Empty;
        public bool IsPermitted { get; set; }
    }
}
