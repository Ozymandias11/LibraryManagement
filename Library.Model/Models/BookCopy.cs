using Library.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model.Models
{
    public class BookCopy
    {
        public Guid BookCopyId { get; set; }
        public int NumberOfPages { get; set; }
        public Status Status { get; set; }
        public Guid OriginaBookId { get; set; }
        public Book? OriginalBook { get; set; }
        public ICollection<Reservation>? Reservations { get; set; }
        public ICollection<BookCopyShelf>? Shelves { get; set; }
        


    }
}
