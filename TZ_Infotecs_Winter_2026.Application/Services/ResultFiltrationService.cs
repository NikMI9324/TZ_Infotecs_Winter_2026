using Microsoft.EntityFrameworkCore;
using TZ_Infotecs_Winter_2026.Application.Dtos;
using TZ_Infotecs_Winter_2026.Application.Interfaces;
using TZ_Infotecs_Winter_2026.Application.IQuerableExtensions;
using TZ_Infotecs_Winter_2026.Domain.Entities;
using TZ_Infotecs_Winter_2026.Infrastructure.Data;

namespace TZ_Infotecs_Winter_2026.Application.Services
{
    public class ResultFiltrationService : IResultFiltrationService
    {
        private readonly MyAppContext _context;
        public ResultFiltrationService(MyAppContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Result>> GetFilteredDataAsync(ResultFilterDto filterDto)
        {
            return await _context.Results
                .ApplyEqualityFilter(r => r.FileName, filterDto.FileName)
                .ApplyRangeFilter(r => r.AverageValueDefinition, filterDto.AvgValueFrom, filterDto.AvgValueTo)
                .ApplyRangeFilter(r => r.AverageExecutionTime, filterDto.AvgExecTimeFrom, filterDto.AvgExecTimeTo)
                .ApplyRangeFilter(r => r.MinimalDate, filterDto.MinimalDateFrom, filterDto.MinimalDateTo)
                .ToListAsync();
        }
    }
}
