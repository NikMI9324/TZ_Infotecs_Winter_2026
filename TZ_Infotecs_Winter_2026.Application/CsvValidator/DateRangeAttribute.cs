using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace TZ_Infotecs_Winter_2026.Application.CsvValidator
{
    public class DateRangeAttribute : ValidationAttribute
    {
        private readonly DateTime _minDate = new DateTime(2000, 1, 1);
        private readonly DateTime _maxDate = DateTime.Now;
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if (value is not DateTime dateTime)
                return new ValidationResult(ErrorMessage);

            if (dateTime < _minDate)
                return new ValidationResult(ErrorMessage);
            if (dateTime > _maxDate)
                return new ValidationResult(ErrorMessage);
            return ValidationResult.Success;
        }
    }
}
