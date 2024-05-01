using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model.Models
{
   public class Shelf
    {

        
        public Guid RoomId { get; set; }
        public Guid ShelfId { get; set; }
        public int MaxCapacity { get; set; }    

        public Room Room { get; set; } = null!;

        public ICollection<BookCopyShelf>? Books { get; set; }

    }
}
