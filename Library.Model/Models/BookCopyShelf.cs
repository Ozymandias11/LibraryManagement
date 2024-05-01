using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model.Models
{
    public class BookCopyShelf
    {

        public Guid BookCopyId { get; set; } 
        public Guid RoomId { get; set; }    
        public Guid ShelfId { get; set; }   

        public BookCopy? BookCopy { get; set; }
        public Shelf? Shelf { get; set; } 
   
    }
}
