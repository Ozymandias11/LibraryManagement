

using Library.Service.Dto.Library.Dto;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.ViewModels.Library.ViewModels
{
    public class CreateBookViewModel
    {
        [Required(ErrorMessage = "Book Title is required")]
        public string? Title { get; set; }
        public DateTime PublishedYear { get; set; }
        public int Edition { get; set; }

        public IEnumerable<Guid> SelectedAuthorIds { get; set; }

        public IEnumerable<Guid> SelectedPublisherIds { get; set; }

        public IEnumerable<Guid> SelectedCategoryIds{ get; set; }


    }
}
