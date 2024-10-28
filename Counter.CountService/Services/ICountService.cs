using Counter.Shared.DTOs;

namespace Counter.CountService.Services
{
    public interface ICountService
    {
        Task<CountDTO?> GetSonOlcumAsync(string seriNumarasi);
        Task<string> AddOlcumAsync(CountDTO olcum);
    }
}