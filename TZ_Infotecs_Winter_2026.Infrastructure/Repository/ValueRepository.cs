using Microsoft.EntityFrameworkCore;
using TZ_Infotecs_Winter_2026.Domain.Entities;
using TZ_Infotecs_Winter_2026.Domain.Interfaces;
using TZ_Infotecs_Winter_2026.Infrastructure.Data;

namespace TZ_Infotecs_Winter_2026.Infrastructure.Repository
{
    public class ValueRepository : IValueRepository
    {
        private readonly MyAppContext _context;
        public ValueRepository(MyAppContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Value>> AddRangeAsync(IEnumerable<Value> values, string fileName)
        {
            if(await FileExistsAsync(fileName))
                await _context.Values.Where(v => v.FileName == fileName).ExecuteDeleteAsync();
            
            await _context.Values.AddRangeAsync(values);
            await _context.SaveChangesAsync();
            
            return values;
        }

        private async Task<bool> FileExistsAsync(string fileName)
        {
            return await _context.Values.AnyAsync(v => v.FileName == fileName);
        }

        public async Task<IEnumerable<Value>> GetLastValuesByFileNameAsync(string fileName, int count = 10)
        {
            return await _context.Values
               .Where(v => v.FileName == fileName)
               .OrderByDescending(v => v.Date)
               .Take(count)
               .ToListAsync();
        }
    }
}
