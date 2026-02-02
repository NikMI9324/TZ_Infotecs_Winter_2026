using TZ_Infotecs_Winter_2026.Domain.Entities;

namespace TZ_Infotecs_Winter_2026.Domain.Interfaces
{
    public interface IResultRepository
    {
        Task AddOrUpdateAsync(Result result);
    }
}
