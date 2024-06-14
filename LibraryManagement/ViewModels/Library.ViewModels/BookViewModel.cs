namespace LibraryManagement.ViewModels.Library.ViewModels
{
    public class BookViewModel
    {
        public Guid BookId { get; set; }
        public string? Title { get; set; }
        public DateTime PublishedYear { get; set; }
    }
}
