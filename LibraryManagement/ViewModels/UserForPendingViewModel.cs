namespace LibraryManagement.ViewModels
{
    public class UserForPendingViewModel
    {
       
        public string Id { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool EmailConfirmed { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
