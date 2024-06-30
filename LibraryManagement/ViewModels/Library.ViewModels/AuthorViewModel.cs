namespace LibraryManagement.ViewModels.Library.ViewModels
{
    public class AuthorViewModel
    {
        public Guid AuthorId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? ErrorMessage { get; set; }    

    }
}
