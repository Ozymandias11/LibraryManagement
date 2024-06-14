using Library.Model.Enums;
using Library.Model.Models;

namespace LibraryManagement.ViewModels.Library.ViewModels
{
    public class CreateBookCopyViewModel
    {
        public IEnumerable<BookViewModel> Books { get; set; }
        public Guid SelectedBookId { get; set; }
        public IEnumerable<PublisherViewModel> Publishers { get; set; }
        public Guid SelectedPublisherId { get; set; }
        public IEnumerable<RoomViewModel> Rooms { get; set; }    
        public Guid SelectedRoomId { get; set; }
        public IEnumerable<ShelfViewModel> Shelves { get; set; }    
        public Guid SelectedShelfId { get; set; }  
        public int NumberOfPages { get; set; }
        public string Edition { get; set; }
        public Status Status { get; set; }
        public int Quantity { get; set; }

    }
}
