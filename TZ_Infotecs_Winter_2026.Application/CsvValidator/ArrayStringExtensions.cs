using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text;

namespace TZ_Infotecs_Winter_2026.Application.CsvValidator
{
    public static class ArrayStringExtensions
    {
        private const string DateFormat = "yyyy-MM-dd'T'HH-mm-ss.ffff'Z'";
        private static ValidationResult ValidateCsvRow(
            this string[] parts, 
            out DateTime date,
            out int executionTime,
            out double value)
        {
            date = default;
            executionTime = default;
            value = default;

            if (parts.Length != 3)
                return new ValidationResult("Строка не соответствует заданному количеству значений." 
                                            + $"Ожидается 3, получено {parts.Length}: " +
                                                string.Join(";", parts));

            if (!DateTime.TryParseExact(parts[0], DateFormat, CultureInfo.InvariantCulture,
                                      DateTimeStyles.AssumeUniversal, out date))
                return new ValidationResult($"Неверный формат даты: '{parts[0]}'."
                    + $" Ожидается формат: {DateFormat}");

            if (!int.TryParse(parts[1], out executionTime))
                return new ValidationResult($"Неверный формат времени выполнения: '{parts[1]}'. " 
                    + "Ожидается целое число.");

            if (!double.TryParse(parts[2], NumberStyles.Any, CultureInfo.InvariantCulture, out value))
                return new ValidationResult($"Неверный формат значения показателя: '{parts[2]}'. Ожидается число.");

           

            return ValidationResult.Success;
        }

        public static bool TryParseToCsvRow(
            this string[] parts, 
            out CsvRow row, 
            out List<ValidationResult> validationResults)
        {
            row = null;
            validationResults = new List<ValidationResult>();
            var basicResult = parts.ValidateCsvRow(out var date, out var execTime, out var val);
            if (basicResult != ValidationResult.Success)
            {
                validationResults.Add(basicResult);
                return false;
            }

            try
            {
                row = new CsvRow(date, execTime, val);
                var validationContext = new ValidationContext(row);
                if (!Validator.TryValidateObject(row, validationContext, validationResults, true))
                {
                    row = null;
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                validationResults.Add(new ValidationResult($"Внутренняя ошибка при создании объекта: {ex.Message}"));
                row = null;
                return false;
            }
        }
        public static bool TryParseToCsvRow(
            this string[] parts,
            out CsvRow row
            )
        {
            return parts.TryParseToCsvRow(out row, out _);
        }
    }
}
