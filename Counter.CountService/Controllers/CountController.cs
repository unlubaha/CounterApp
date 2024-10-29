using Counter.CountService.Services;
using Counter.Entities;
using Counter.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Counter.CountService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CountController : ControllerBase
    {
        private readonly ICountService _countService;

        public CountController(ICountService countService)
        {
            _countService = countService;
        }

        [HttpGet("LastCount")]
        public async Task<IActionResult> GetLastCount(string seriNumarasi)
        {
            var Count = await _countService.GetLastCountAsync(seriNumarasi);
            return Count != null ? Ok(Count) : NotFound("Ölçüm bulunamadı.");
        }

        [HttpPost("AddCount")]
        public async Task<IActionResult> AddCount([FromBody] CountRequestDTO Count)
        {
            if (Count == null)
            {
                return BadRequest("Geçersiz ölçüm verisi.");
            }

            if (string.IsNullOrEmpty(Count.SeriNumarasi) || Count.SeriNumarasi.Length != 8)
            {
                return BadRequest("Seri numarası 8 karakter olmalıdır.");
            }

            var result = await _countService.AddCountAsync(Count);
            return Ok(result);
        }
    }
}