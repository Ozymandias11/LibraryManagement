namespace LibraryManagement.ViewModels.Library.ViewModels
{
    public class BookCopyReservationRequestViewModel
    {
        public Guid OriginalBookId { get; set; }
        public string? Edition { get; set; }
        public Guid PublisherId { get; set; }
        public int Quantity { get; set; }
    }
}
