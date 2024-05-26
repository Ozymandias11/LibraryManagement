using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Validators
{

    public class RequireAtLeastOneRoleAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is not ICollection<string> selectedRoles || selectedRoles.Count == 0)
            {
                return new ValidationResult("Please select at least one role.");
            }
            return ValidationResult.Success;
        }
    }

}
