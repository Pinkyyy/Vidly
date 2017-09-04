using System;
using System.ComponentModel.DataAnnotations;
using Vidly.Models;
using MembershipType = Vidly.Enums.MembershipType;

namespace Vidly.AttributeValidators
{
    public class Min18YearsIfAMemberValidator : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var customer = (Customer)validationContext.ObjectInstance;
            if (customer.MembershipTypeId == (int)MembershipType.None || customer.MembershipTypeId == (int)MembershipType.PayAsYouGo)
            {
                return ValidationResult.Success;
            }

            if (customer.BirthDate == null)
            {
                return new ValidationResult("Birth date is required");
            }

            var age = DateTime.Today.Year - customer.BirthDate.Value.Year;
            return (age >= 18)
                ? ValidationResult.Success
                : new ValidationResult("Customer should be at least 18 years old to go on a membership");

            //return base.IsValid(value, validationContext);
        }
    }
}