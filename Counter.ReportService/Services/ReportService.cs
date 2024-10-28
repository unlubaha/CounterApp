using Counter.Entities;
using Counter.Shared.DTOs;
using Counter.Shared.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Counter.ReportService.Services
{
    public class ReportService : IReportService
    {
        private readonly ApplicationDbContext _context;

        public ReportService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<string> CreateReportAsync(string seriNumarasi)
        {
            var olcumler = await _context.Counters
                .Where(o => o.SeriNumarasi == seriNumarasi)
                .OrderByDescending(o => o.OlcumZamani)
                .ToListAsync();

            if (!olcumler.Any())
            {
                return "Rapor oluşturmak için yeterli veri yok.";
            }

            var rapor = new ReportDTO
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

            _context.Reports.Add(rapor);
            await _context.SaveChangesAsync();

            return "Rapor başarıyla oluşturuldu.";
        }

        public async Task<IEnumerable<ReportDTO>> GetReportsAsync()
        {
            return await _context.Reports.ToListAsync();
        }
        public async Task<ReportDTO?> GetReportByIdAsync(Guid uuid)
        {
            return await _context.Reports.FindAsync(uuid);
        }
    }
}