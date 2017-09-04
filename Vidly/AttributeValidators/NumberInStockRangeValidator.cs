using System.ComponentModel.DataAnnotations;
using Vidly.Models;

namespace Vidly.AttributeValidators
{
    public class NumberInStockRangeValidator : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var movie = (Movie)validationContext.ObjectInstance;
            return movie.NumberInStock <= 20
                ? ValidationResult.Success
                : new ValidationResult("Range has to be between 0 and 20");
        }
    }
}