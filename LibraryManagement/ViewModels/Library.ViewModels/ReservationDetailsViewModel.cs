using Library.Service.Dto.Library.Dto;

namespace LibraryManagement.ViewModels.Library.ViewModels
{
    public class ReservationDetailsViewModel
    {
        public string? CustomerFullName { get; set; }
        public string? EmployeeFullName { get; set; }
        public DateTime CheckoutTime { get; set; }
        public DateTime SupposedReturnDate { get; set; }
        public DateTime? ActualReturnDate { get; set; }
        public bool IsLate { get; set; }
        public List<ReservationItemForDetailsDto>? ReservationItems { get; set; }
    }
}
