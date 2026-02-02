using TZ_Infotecs_Winter_2026.Domain.Entities;
using TZ_Infotecs_Winter_2026.Domain.Interfaces;
using TZ_Infotecs_Winter_2026.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace TZ_Infotecs_Winter_2026.Infrastructure.Repository
{
    public class ResultRepository : IResultRepository
    {
        private readonly MyAppContext _context;
        public ResultRepository(MyAppContext context)
        {
            _context = context;
        }

        public async Task AddOrUpdateAsync(Result result)
        {
            if (result == null || string.IsNullOrEmpty(result.FileName))
                throw new InvalidDataException("Отправлены пустые данные либо не указано имя файла для сохранения");
            var existingResult = await _context.Results.FindAsync(result.FileName);
            if (existingResult != null)
                await UpdateAsync(result);
            else
                await AddAsync(result);

        }

        private async Task AddAsync(Result result)
        {
            await _context.Results.AddAsync(result);
            await _context.SaveChangesAsync();
        }
        private async Task UpdateAsync(Result result)
        {
            await _context.Results.Where(r => r.FileName == result.FileName)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(r => r.TimeDeltaSeconds, result.TimeDeltaSeconds)
                    .SetProperty(r => r.MinimalDate, result.MinimalDate)
                    .SetProperty(r => r.AverageExecutionTime, result.AverageExecutionTime)
                    .SetProperty(r => r.AverageValueDefinition, result.AverageValueDefinition)
                    .SetProperty(r => r.MedianValueDefinition, result.MedianValueDefinition)
                    .SetProperty(r => r.MaxValueDefinition, result.MaxValueDefinition)
                    .SetProperty(r => r.MinValueDefinition, result.MinValueDefinition)
                );
        }
    }
}
