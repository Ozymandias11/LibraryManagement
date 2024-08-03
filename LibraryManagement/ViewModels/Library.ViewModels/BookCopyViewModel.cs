using Library.Model.Enums;
using Library.Model.Models;

namespace LibraryManagement.ViewModels.Library.ViewModels
{
    public class BookCopyViewModel
    {
        public Guid BookCopyId { get; set; }
        public int NumberOfPages { get; set; }
        public Status Status { get; set; }
        public string Edition { get; set; }
        public int Quantity { get; set; }
        public Guid PublisherId { get; set; }
        public string PublisherName { get; set; } 
        public Guid OriginaBookId { get; set; }
        public string BookTitle { get; set; }
        public int RoomNumber { get; set; }
        public int ShelfNumber { get; set; }
        public Guid RoomId { get; set; }
        public Guid ShelfId { get; set; }
        public Guid BookCopyShelfId { get; set; }


    }
}
