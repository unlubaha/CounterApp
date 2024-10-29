using Counter.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Counter.Shared.DTOs;
using Counter.Shared.Enums;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

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

        [HttpPost("CreateReport")]
        public IActionResult CreateReport([FromBody] string seriNumarasi)
        {
            var olcumler = _context.Counters
                .Where(o => o.SeriNumarasi == seriNumarasi)
                .OrderByDescending(o => o.OlcumZamani).ToList();

            if (!olcumler.Any())
                return NotFound("Veri bulunamadı.");

            var rapor = new ReportDTO()
            {
                UUID = Guid.NewGuid(),
                TalepTarihi = DateTime.Now,
                Durum = RaporDurumu.Hazirlaniyor,
                Icerik = new Icerik
                {
                    OlcumZamani = olcumler.First().OlcumZamani,
                    SonEndeks = (int)olcumler.First().SonEndeks,
                    Voltaj = olcumler.First().Voltaj,
                    Akim = olcumler.First().Akim
                }
            };

            try
            {
                _context.Reports.Add(rapor);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Rapor oluşturulurken hata oluştu: " + ex.Message);
            }

            return Ok("Rapor isteği başarıyla oluşturuldu.");
        }

        [HttpGet]
        public IActionResult GetReports()
        {
            var raporlar = _context.Reports.ToList();
            return Ok(raporlar);
        }

        [HttpGet("{uuid}")]
        public IActionResult GetReportDetail(Guid uuid)
        {
            var rapor = _context.Reports.Find(uuid);
            return rapor != null ? Ok(rapor) : NotFound("Rapor bulunamadı.");
        }
    }
}