using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model.Models
{
    public class Room : BaseModel
    {
        public Guid RoomId{ get; set; }
        public int Capacity { get; set; }

        public int RoomNumber { get; set; } 

        public ICollection<Employee>? Employees { get; set; }
        public ICollection<Shelf>? Shelves { get; set; }

    }
}
