using System.ComponentModel.DataAnnotations;

namespace LibraryManagement
{
    public class EnsureNotEmptyAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is IEnumerable<Guid> list && list.Any())
            {
                return ValidationResult.Success;
            }
            return new ValidationResult(ErrorMessage ?? "The list cannot be empty.");
        }
    }
}
