using Library.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Service.Dto.Library.Dto
{

    public class BookCopyDto
    {
        public Guid BookCopyId { get; set; }
        public int NumberOfPages { get; set; }
        public Status Status { get; set; }
        public string Edition { get; set; }
        public int Quantity { get; set; }
        public Guid PublisherId { get; set; }
        public string PublisherName { get; set; }
        public string BookTitle { get; set; }
        public Guid OriginaBookId { get; set; }
        public int RoomNumber { get; set; }
        public int ShelfNumber { get; set; }
        public Guid RoomId { get; set; }
        public Guid ShelfId { get; set; }
        public Guid BookCopyShelfId { get; set; }
    }

}
