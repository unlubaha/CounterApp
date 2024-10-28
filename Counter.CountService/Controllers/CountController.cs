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

        [HttpGet("{seriNumarasi}/son-olcum")]
        public async Task<IActionResult> GetSonOlcum(string seriNumarasi)
        {
            var olcum = await _countService.GetSonOlcumAsync(seriNumarasi);
            return olcum != null ? Ok(olcum) : NotFound("Ölçüm bulunamadı.");
        }

        [HttpPost("olcum-ekle")]
        public async Task<IActionResult> AddOlcum([FromBody] CountDTO olcum)
        {
            if (olcum == null)
            {
                return BadRequest("Geçersiz ölçüm verisi.");
            }

            if (string.IsNullOrEmpty(olcum.SeriNumarasi) || olcum.SeriNumarasi.Length != 8)
            {
                return BadRequest("Seri numarası 8 karakter olmalıdır.");
            }

            var result = await _countService.AddOlcumAsync(olcum);
            return Ok(result);
        }
    }
}