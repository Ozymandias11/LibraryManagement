namespace LibraryManagement.ViewModels.Library.ViewModels
{
    public class CreateReservationViewModel
    {
        public Guid CustomerID { get; set; }
        public DateTime CheckoutTime { get; set; }
        public DateTime SupposedReturnDate { get; set; }
        public string? EmployeeId { get; set; }
        public IEnumerable<BookCopyReservationRequestViewModel>? BookCopyReservations { get; set; }
    }
}
