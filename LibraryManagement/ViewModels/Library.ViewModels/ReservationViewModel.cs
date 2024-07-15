namespace LibraryManagement.ViewModels.Library.ViewModels
{
    public class ReservationViewModel
    {
        public Guid ReservationId { get; set; }
        public string? CustomerPersonalID { get; set; }
        public DateTime CheckoutTime { get; set; }
        public DateTime SupposedReturnDate { get; set; }
        public DateTime? ActualReturnDate { get; set; }
        public Guid? ReturnCustomerId { get; set; }
        public bool IsLate { get; set; }
        public string? EmployeeEmail { get; set; }
        public IEnumerable<ReservationItemViewModel>? ReservationItems { get; set; }
        // properties for paging 
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
    }
}
