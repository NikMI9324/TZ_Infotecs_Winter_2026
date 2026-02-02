using System;
using System.Collections.Generic;
using System.Text;
using TZ_Infotecs_Winter_2026.Domain.Entities;

namespace TZ_Infotecs_Winter_2026.Domain.Interfaces
{
    public interface IValueRepository
    {
        Task<IEnumerable<Value>> AddRangeAsync(IEnumerable<Value> values, string fileName);
        Task<IEnumerable<Value>> GetLastValuesByFileNameAsync(string fileName, int count = 10);
    }
}
