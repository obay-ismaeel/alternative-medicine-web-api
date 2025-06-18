using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Text.RegularExpressions;

namespace AlternativeMedicine.App.Domain.Attributes;

public class ValidColorAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value == null)
        {
            return ValidationResult.Success;
        }

        try
        {
            var color = ColorTranslator.FromHtml(value.ToString());

            return ValidationResult.Success;
        }
        catch (Exception)
        {
            return new ValidationResult($"The {validationContext.DisplayName} field is not a valid color.");    
        }
    }
}
