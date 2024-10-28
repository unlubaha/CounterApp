using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Counter.CountService.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class CountController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CountController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("{seriNumarasi}/son-olcum")]
        public IActionResult GetSonOlcum(string seriNumarasi)
        {
            var olcum = _context.SayacOlcumler
                .Where(o => o.SeriNumarasi == seriNumarasi)
                .OrderByDescending(o => o.OlcumZamani)
                .FirstOrDefault();
            return olcum != null ? Ok(olcum) : NotFound("Ölçüm bulunamadı.");
        }

        [HttpPost("olcum-ekle")]
        public IActionResult AddOlcum([FromBody] SayacOlcum olcum)
        {
            _context.SayacOlcumler.Add(olcum);
            _context.SaveChanges();
            return Ok("Ölçüm başarıyla eklendi.");
        }
    }

}

