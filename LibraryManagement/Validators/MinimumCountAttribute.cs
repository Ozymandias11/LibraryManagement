using System.ComponentModel.DataAnnotations;

public class MinimumCountAttribute : ValidationAttribute
{
    private readonly int _minCount;

    public MinimumCountAttribute(int minCount)
    {
        _minCount = minCount;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var enumerable = value as IEnumerable<object>;
        if (enumerable != null && enumerable.Count() >= _minCount)
        {
            return ValidationResult.Success;
        }

        return new ValidationResult(ErrorMessage);
    }
}