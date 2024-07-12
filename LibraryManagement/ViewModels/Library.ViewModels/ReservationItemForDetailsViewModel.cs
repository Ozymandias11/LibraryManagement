namespace LibraryManagement.ViewModels.Library.ViewModels
{
    public class ReservationItemForDetailsViewModel
    {
        public string? BookTitle { get; set; }
        public string? Edition { get; set; }
        public string? PublisherName { get; set; }
        public int Quantity { get; set; }
        public DateTime? ActualReturnDate { get; set; }
    }
}
