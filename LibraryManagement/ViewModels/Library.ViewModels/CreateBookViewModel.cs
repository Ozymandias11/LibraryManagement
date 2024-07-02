

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
        public IEnumerable<AuthorViewModel> Authors { get; set; }
        public IEnumerable<PublisherViewModel> Publishers { get; set; }
        public IEnumerable<CategoryViewModel> Categories { get; set; }


       // [MinimumCount(1, ErrorMessage = "Please select at least one Author")]
        public IEnumerable<Guid> SelectedAuthorIds { get; set; } 

      //  [MinimumCount(1, ErrorMessage = "Please select at least one Author")]
        public IEnumerable<Guid> SelectedPublisherIds { get; set; } 

       // [MinimumCount(1, ErrorMessage = "Please select at least one Author")]
        public IEnumerable<Guid> SelectedCategoryIds{ get; set; }


    }
}
