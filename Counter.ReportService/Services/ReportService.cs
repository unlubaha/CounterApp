using Counter.CountService.Services;
using Counter.Entities;
using Counter.Shared.DTOs;
using Counter.Shared.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Text.Json;

namespace Counter.ReportService.Services
{
    public class ReportService : IReportService
    {
        private readonly ApplicationDbContext _context;
        private readonly RabbitMqService _rabbitMqService;

        public ReportService(ApplicationDbContext context, RabbitMqService rabbitMqService)
        {
            _context = context;
            _rabbitMqService = rabbitMqService;
        }

        public async Task<string> CreateReportAsync(string seriNumarasi)
        {
            var olcumler = await _context.Counters.GetService<ICountService>().GetCountsAsync(seriNumarasi);

            if (!olcumler.Any())
            {
                return "Rapor oluşturmak için yeterli veri yok.";
            }

            var rapor = new ReportRequestDTO()
            {
                Durum = RaporDurumu.Hazirlaniyor,
                Icerik = new Icerik
                {
                    OlcumZamani = olcumler.First().OlcumZamani,
                    SonEndeks = olcumler.First().SonEndeks,
                    Voltaj = olcumler.First().Voltaj,
                    Akim = olcumler.First().Akim
                }
            };

            _context.Reports.Add(rapor);
            await _context.SaveChangesAsync();

            _rabbitMqService.SendMessage(rapor);

            await UpdateReportStatus(rapor.UUID, RaporDurumu.Hazirlaniyor);

            return "Rapor başarıyla oluşturuldu ve kuyrukta yer aldı.";
        }

        public async Task UpdateReportStatus(Guid uuid, RaporDurumu durum)
        {
            var report = await _context.Reports.FindAsync(uuid);
            if (report != null)
            {
                report.Durum = durum;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<ReportRequestDTO>> GetReportsAsync()
        {
            return await _context.Reports.ToListAsync();
        }

        public async Task<ReportRequestDTO?> GetReportByIdAsync(Guid uuid)
        {
            return await _context.Reports.FindAsync(uuid);
        }
    }
}
