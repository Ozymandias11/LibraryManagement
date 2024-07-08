namespace LibraryManagement.ViewModels.Library.ViewModels
{
    public class ReservationViewModel
    {
        public Guid ReservationId { get; set; }
        public Guid CustomerID { get; set; }
        public DateTime CheckoutTime { get; set; }
        public DateTime SupposedReturnDate { get; set; }
        public DateTime? ActualReturnDate { get; set; }
        public Guid? ReturnCustomerId { get; set; }
        public bool IsLate { get; set; }
        public string? EmployeeId { get; set; }
        public IEnumerable<ReservationItemViewModel>? ReservationItems { get; set; }
    }
}
