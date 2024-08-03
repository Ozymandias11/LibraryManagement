namespace LibraryManagement.ViewModels.Library.ViewModels
{
    public class ModifyBookCopiesViewModel
    {
        public Guid OriginalBookId { get; set; }    
        public Guid PublisherId { get; set; }
        public string Edition { get; set; } 
        public string State {  get; set; }  
        public int QuantityModified { get; set; }
        public string Message { get; set; }
        public Guid RoomId { get; set; }
        public Guid ShelfId { get; set; }
        public Guid BookCopyShelfId { get; set; }
    }
}
