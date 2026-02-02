using System.ComponentModel.DataAnnotations;

namespace TZ_Infotecs_Winter_2026.Application.CsvValidator
{
    public record CsvRow
    {
        [DateRange(ErrorMessage = "Значение даты не должно быть раньше 01.01.2000 и не позже текущей даты")]
        public DateTime Date { get; init; }
        [Range(0, int.MaxValue, ErrorMessage = "Значение вермени выполнения не должно быть отрицательным")]
        public int ExecutionTime { get; init; }
        [Range(0, double.MaxValue, ErrorMessage = "Значение показателя не должно быть отрицательным")]
        public double Value { get; init; }
        public CsvRow(DateTime date, int executionTime, double value)
        {
            Date = date;
            ExecutionTime = executionTime;
            Value = value;
        }

    }
}
