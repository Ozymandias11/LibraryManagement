using Library.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model.Models
{
    public class BookCopy : BaseModel
    {
        public Guid BookCopyId { get; set; }
        public int NumberOfPages { get; set; }
        public Status Status { get; set; }
        public string Edition { get; set; }
        public int Quantity { get; set; }
        public Guid PublisherId { get; set; }
        public Publisher? Publisher { get; set; }
        public Guid OriginaBookId { get; set; }
        public Book? OriginalBook { get; set; }
        public ICollection<Reservation>? Reservations { get; set; }
        public ICollection<BookCopyShelf>? Shelves { get; set; }
        


    }
}
