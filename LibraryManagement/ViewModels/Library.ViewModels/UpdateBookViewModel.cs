using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.ViewModels.Library.ViewModels
{
    public class UpdateBookViewModel
    {
        public Guid BookId { get; set; }
        public string? Title { get; set; }
        public DateTime PublishedYear { get; set; }
        public int Edition { get; set; }
        public IEnumerable<AuthorViewModel> Authors { get; set; }
        public IEnumerable<PublisherViewModel> Publishers { get; set; }
        public IEnumerable<CategoryViewModel> Categories { get; set; }

        public IEnumerable<Guid> SelectedAuthorIds { get; set; } = new List<Guid>();
        public IEnumerable<Guid> SelectedPublisherIds { get; set; } = new List<Guid>();
        public IEnumerable<Guid> SelectedCategoryIds { get; set; } = new List<Guid>();
    }
}
