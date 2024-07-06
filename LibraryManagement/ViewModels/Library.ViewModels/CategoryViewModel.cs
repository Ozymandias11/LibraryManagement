using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.ViewModels.Library.ViewModels
{
    public class CategoryViewModel
    {
        public Guid CategoryId { get; set; }
        [Required(ErrorMessage = "Category Title is required")]
        public string? Title { get; set; }

    }
}
