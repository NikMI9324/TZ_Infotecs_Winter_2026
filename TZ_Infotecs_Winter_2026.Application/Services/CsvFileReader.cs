using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using TZ_Infotecs_Winter_2026.Application.CsvValidator;
using TZ_Infotecs_Winter_2026.Application.Interfaces;
using TZ_Infotecs_Winter_2026.Domain.Entities;
using TZ_Infotecs_Winter_2026.Domain.Interfaces;
using TZ_Infotecs_Winter_2026.Infrastructure.Data;

namespace TZ_Infotecs_Winter_2026.Application.Services
{
    public class CsvFileReader : ICsvFileReader
    {
        private readonly IValueRepository _valueRepo;
        private readonly IResultRepository _resultRepo;
        private readonly MyAppContext _context;
        public CsvFileReader(IValueRepository valueRepo, 
                            IResultRepository resultRepo, 
                            MyAppContext context)
        {
            _valueRepo = valueRepo;
            _resultRepo = resultRepo;
            _context = context;
        }

        public async Task ParseCvsFileAsync(IFormFile file)
        {
            if (file is null || file.Length == 0)
                throw new ValidationException("Файл не предоставлен");
            var values = new List<Value>();
            using var reader = new StreamReader(file.OpenReadStream());

            await using var transaction = await _context.Database.BeginTransactionAsync();
            while (!reader.EndOfStream)
            {
                if (values.Count > 10_000)
                    throw new ValidationException("Количество строк превысило заданного значения");

                var line = await reader.ReadLineAsync();
                var parts = line.Split(";");

                if (!parts.TryParseToCsvRow(out var row, out var validationResults))
                    throw new ValidationException(string.Join("\n", validationResults.Select(v => v.ErrorMessage)));

                var value = new Value
                {
                    Id = Guid.NewGuid(),
                    Date = row.Date,
                    ExecutionTime = row.ExecutionTime,
                    ValueDefinition = row.Value,
                    FileName = file.FileName

                };
                values.Add(value);
            }

            if (values.Count == 0)
                throw new ValidationException("В файле должна быть хотя бы одна запись");

            await _valueRepo.AddRangeAsync(values, file.FileName);
            var result = new Result
            {
                FileName = file.FileName,
                TimeDeltaSeconds = (values.Max(v => v.Date) - values.Min(v => v.Date)).TotalSeconds,
                AverageExecutionTime = values.Average(v => v.ExecutionTime),
                AverageValueDefinition = values.Average(v => v.ValueDefinition),
                MedianValueDefinition = GetMedian(values.Select(v => v.ValueDefinition)),
                MaxValueDefinition = values.Max(v => v.ValueDefinition),
                MinValueDefinition = values.Min(v => v.ValueDefinition),
                MinimalDate = values.Min(v => v.Date)
            };
            await _resultRepo.AddOrUpdateAsync(result);

            await transaction.CommitAsync();
        }
        private static double GetMedian(IEnumerable<double> values)
        {
            var sorted = values.OrderBy(x => x).ToList();
            if (sorted.Count == 0)
                return 0;
            return sorted.Count % 2 == 0 ? (sorted[sorted.Count / 2 - 1]
                + sorted[sorted.Count / 2]) / 2 : sorted[sorted.Count / 2];
        }
    }
}
