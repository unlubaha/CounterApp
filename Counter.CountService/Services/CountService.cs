using System.Diagnostics.Metrics;
using Counter.Entities;
using Counter.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Counter.CountService.Services
{
    public class CountService : ICountService
    {
        private readonly ApplicationDbContext _context;

        public CountService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<CountResponseDTO?> GetLastCountAsync(string seriNumarasi)
        {
            if (string.IsNullOrWhiteSpace(seriNumarasi))
            {
                throw new ArgumentException("Seri numarası boş olamaz.");
            }

            return await _context.Counters
                .Where(o => o.SeriNumarasi == seriNumarasi)
                .OrderByDescending(o => o.OlcumZamani)
                .Select(o => new CountResponseDTO
                {
                    UUID = o.UUID,
                    SeriNumarasi = o.SeriNumarasi,
                    OlcumZamani = o.OlcumZamani,
                    SonEndeks = o.SonEndeks,
                    Voltaj = o.Voltaj,
                    Akim = o.Akim
                })
                .FirstOrDefaultAsync();
        }
        public async Task<string> AddCountAsync(CountRequestDTO count)
        {
            try
            {
                _context.Counters.Add(count);
                await _context.SaveChangesAsync();

                return "Ölçüm başarıyla eklendi.";
            }
            catch (Exception ex)
            {
                return "Ölçüm eklenirken bir hata oluştu:" + ex.Message;
            }
        }
        public async Task<List<CountResponseDTO>> GetCountsAsync(string seriNumarasi)
        {
            if (string.IsNullOrWhiteSpace(seriNumarasi))
            {
                throw new ArgumentException("Seri numarası boş olamaz.");
            }
            return await _context.Counters
                .Where(m => m.SeriNumarasi == seriNumarasi)
                .OrderByDescending(m => m.OlcumZamani)
                .Select(m => new CountResponseDTO
                {
                    UUID = m.UUID,
                    SeriNumarasi = m.SeriNumarasi,
                    OlcumZamani = m.OlcumZamani,
                    SonEndeks = m.SonEndeks,
                    Voltaj = m.Voltaj,
                    Akim = m.Akim
                })
                .ToListAsync();
        }
    }
}