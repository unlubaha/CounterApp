using Counter.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Counter.ReportService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ReportController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("olustur")]
        public IActionResult CreateRapor([FromBody] string seriNumarasi) // [FromBody] kullanabilirsiniz
        {
            var olcumler = _context.SayacOlcumler
                .Where(o => o.SeriNumarasi == seriNumarasi)
                .OrderByDescending(o => o.OlcumZamani).ToList();

            if (!olcumler.Any())
                return NotFound("Veri bulunamadı.");

            var rapor = new Rapor
            {
                UUID = Guid.NewGuid(),
                TalepTarihi = DateTime.Now,
                Durum = "Hazırlanıyor",
                Icerik = GenerateRaporContent(olcumler)
            };

            try
            {
                _context.Raporlar.Add(rapor);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Rapor oluşturulurken hata oluştu: " + ex.Message);
            }

            return Ok("Rapor isteği başarıyla oluşturuldu.");
        }

        [HttpGet]
        public IActionResult GetRaporlar()
        {
            var raporlar = _context.Raporlar.ToList();
            return Ok(raporlar);
        }

        [HttpGet("{uuid}")]
        public IActionResult GetRaporDetay(Guid uuid)
        {
            var rapor = _context.Raporlar.Find(uuid);
            return rapor != null ? Ok(rapor) : NotFound("Rapor bulunamadı.");
        }

        private string GenerateRaporContent(List<SayacOlcum> olcumler)
        {
            var header = "Olcum Zamanı, Son Endeks, Voltaj, Akım"; // Başlık ekleyin
            var content = string.Join(Environment.NewLine, olcumler.Select(o =>
                $"{o.OlcumZamani}, {o.SonEndeks}, {o.Voltaj}, {o.Akim}"));
            return $"{header}\n{content}"; // Başlık ile içeriği birleştirin
        }
    }
}
