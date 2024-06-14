using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.ViewModels.Library.ViewModels
{
    public class CreateCategoryViewModel
    {
        [Required(ErrorMessage = "Category Title is Required")]
        public string? Title { get; set; }
    }
}
