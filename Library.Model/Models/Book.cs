using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model.Models
{
   public class Book : BaseModel
    {
        public Guid BookId { get; set; }
        public string? Title { get; set; }  
        public DateTime PublishedYear { get; set; }
        public ICollection<BookAuthor>? Authors { get; set;}
        public ICollection<BookCategory>? Categories { get; set;}
        public ICollection<BookPublisher>? Publishers { get; set;}
        public ICollection<BookCopy>? Copyrights { get; set; }
        public ICollection<BookCopyLog>? BookCopyLogs { get; set; }
    

    }
}
