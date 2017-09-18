using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Memoriser.UnitTests.API
{
    public class ValidatorTests
    {
        protected List<ValidationResult> ValidateObject(object obj)
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(obj, null, null);
            Validator.TryValidateObject(obj, context, results, true);
            return results;
        }
    }
}
