using System.ComponentModel.DataAnnotations;

namespace LibraryManagement
{
    //public class MinimumCountAttribute : ValidationAttribute
    //{
    //    private readonly int _minCount;

    //    public MinimumCountAttribute(int minCount)
    //    {
    //        _minCount = minCount;
    //    }

    //    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    //    {
    //        var enumerable = value as IEnumerable<object>;
    //        if (enumerable != null && enumerable.Count() < _minCount)
    //        {
    //            return new ValidationResult(ErrorMessage);
    //        }

    //        return ValidationResult.Success;
    //    }
    //}
}
