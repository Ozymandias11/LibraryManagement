namespace LibraryManagement.ViewModels.Library.ViewModels
{
    public class ReservationItemViewModel
    {
        public Guid ReservationItemId { get; set; }
        public Guid BookCopyID { get; set; }
        public DateTime? ActualReturnDate { get; set; }
        public string? BookTitle { get; set; }
    }
}
