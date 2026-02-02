using TZ_Infotecs_Winter_2026.Application.Dtos;
using TZ_Infotecs_Winter_2026.Domain.Entities;

namespace TZ_Infotecs_Winter_2026.Application.Interfaces
{
    public interface IResultFiltrationService
    {
        Task<IEnumerable<Result>> GetFilteredDataAsync(ResultFilterDto filterDto);
    }
}
