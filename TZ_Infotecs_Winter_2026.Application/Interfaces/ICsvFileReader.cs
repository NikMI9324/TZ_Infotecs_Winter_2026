using Microsoft.AspNetCore.Http;

namespace TZ_Infotecs_Winter_2026.Application.Interfaces
{
    public interface ICsvFileReader
    {
        Task ParseCvsFileAsync(IFormFile file);
    }
}
