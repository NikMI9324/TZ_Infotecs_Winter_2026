using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using TZ_Infotecs_Winter_2026.Application.Dtos;
using TZ_Infotecs_Winter_2026.Application.Interfaces;
using TZ_Infotecs_Winter_2026.Domain.Interfaces;

namespace TZ_Infotecs_Winter_2026.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppController : ControllerBase
    {
        private readonly IValueRepository _valueRepository;
        private readonly ICsvFileReader _csvFileReader;
        private readonly IResultFiltrationService _resultFiltrationService;

        public AppController(IValueRepository valueRepository, ICsvFileReader csvFileReader, IResultFiltrationService resultFiltrationService)
        {
            _valueRepository = valueRepository;
            _csvFileReader = csvFileReader;
            _resultFiltrationService = resultFiltrationService;
        }
        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            try
            {
                await _csvFileReader.ParseCvsFileAsync(file);
                return Ok(new { Message = $"Файл {file.FileName} успешно обработан." });
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "Внутренняя ошибка сервера.", Details = ex.Message });
            }
        }
        [HttpGet("filter-results")]
        public async Task<IActionResult> FilterResults([FromQuery] ResultFilterDto filterDto)
        {
            try
            {
                var results = await _resultFiltrationService.GetFilteredDataAsync(filterDto);
                return Ok(results);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "Внутренняя ошибка сервера.", Details = ex.Message });
            }
        }
        [HttpGet("values/{fileName}")]
        public async Task<IActionResult> GetValuesByFileName([FromRoute] string fileName)
        {
            try
            {
                var values = await _valueRepository.GetLastValuesByFileNameAsync(fileName);
                return Ok(values);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "Внутренняя ошибка сервера.", Details = ex.Message });
            }
        }
    }
}
