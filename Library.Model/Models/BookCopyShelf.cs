using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model.Models
{
    public class BookCopyShelf : BaseModel
    {

       public Guid BookCopyShelfId { get; set; }
       public Guid RoomId { get; set; }
       public Guid ShelfId { get; set; }
       public Shelf? Shelf { get; set; }
       public ICollection<BookCopy>? BookCopies { get; set; }

   
    }
}
