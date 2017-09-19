using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Memoriser.App.Controllers.PostModels
{
    public class WordArrayAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var array = value as string[];
            if (array == null) return new ValidationResult("Array can't be null");
            if (array.Length == 0) return new ValidationResult("Array can't be empty");
            
            foreach (var str in array)
            {
                if (string.IsNullOrWhiteSpace(str))
                {
                    return new ValidationResult($"Entry {str} is not valid");
                }
                if (!str.IsOnlyLetterCharacters())
                {
                    return new ValidationResult($"Entry {str} is not valid");
                }
            }

            return ValidationResult.Success;
        }
    }
}
