using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.ViewModels.Library.ViewModels
{
    public class CreateCategoryViewModel
    {
        [Required(ErrorMessage = "Category Title in English is Required")]
        public string? TitleEnglish { get; set; }

        [Required(ErrorMessage = "Category Title in German is Required")]
        public string? TitleGerman { get; set; }

    }
}
