namespace LibraryManagement.ViewModels.Library.ViewModels
{
    public class ReturnBookViewModel
    {
        public Guid ReservationId { get; set; }
        public Guid CustomerId { get; set; }
        public List<ReturnActionViewModel>? ReturnItems { get; set; }
    }
}
