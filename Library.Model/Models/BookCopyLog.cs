using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model.Models
{
    public class BookCopyLog : BaseModel
    {
        public Guid LogId { get; set; }
        public Guid OriginalBookId { get; set; }
        public Guid PublishersId { get; set; }
        public string? Edition { get; set; }
        public string? State { get; set; }
        public string? Message { get; set; }
        public int QuantityModified { get; set; }
        public DateTime TimeStamp { get; set; }
        public Book? OriginalBook { get; set; } 
        public Publisher? Publisher { get; set; } 
    }
}
