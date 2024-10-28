using System.Diagnostics.Metrics;
using Counter.Entities;
using Counter.Shared.DTOs;
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

        public async Task<CountDTO?> GetSonOlcumAsync(string seriNumarasi)
        {
            return await _context.Counters
                .Where(o => o.SeriNumarasi == seriNumarasi)
                .OrderByDescending(o => o.OlcumZamani)
                .FirstOrDefaultAsync();
        }
        public async Task<string> AddOlcumAsync(CountDTO olcum)
        {
            try
            {
                var newCounter = new CountDTO
                {
                    SeriNumarasi = olcum.SeriNumarasi,
                    OlcumZamani = DateTime.Now,
                    SonEndeks = olcum.SonEndeks,
                    Voltaj = olcum.Voltaj,
                    Akim = olcum.Akim
                };

                _context.Counters.Add(newCounter);
                await _context.SaveChangesAsync();

                return "Ölçüm başarıyla eklendi.";
            }
            catch (Exception ex)
            {
                // Log the exception (not shown here)
                return "Ölçüm eklenirken bir hata oluştu.";
            }
        }

    }
}