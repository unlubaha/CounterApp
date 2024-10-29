using Counter.Shared.DTOs;

namespace Counter.CountService.Services
{
    public interface ICountService
    {
        Task<CountResponseDTO?> GetLastCountAsync(string seriNumarasi);
        Task<string> AddCountAsync(CountRequestDTO count);
        Task<List<CountResponseDTO>> GetCountsAsync(string seriNumarasi);
    }
}