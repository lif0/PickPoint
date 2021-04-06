using System;
using System.ComponentModel.DataAnnotations;

namespace Shop.Api.Contract.Infrastructure.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class ArrayLengthValidationAttribute : ValidationAttribute
    {
        private readonly int _min;
        private readonly int _max;

        public ArrayLengthValidationAttribute(int min, int max)
        {
            _min = min;
            _max = max;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is string[] array)
            {
                if (array.Length < _min)
                    return new ValidationResult($"Array length must be greater than {_min}");

                if (array.Length > _max)
                    return new ValidationResult($"Array length must not exceed {_max}");
            }
            
            return ValidationResult.Success;
        }
    }
}