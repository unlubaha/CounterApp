using Counter.Shared.DTOs;
using Counter.Shared.Enums;

namespace Counter.ReportService.Services
{
    public interface IReportService
    {
        Task<string> CreateReportAsync(string seriNumarasi);
        Task<IEnumerable<ReportRequestDTO>> GetReportsAsync();
        Task<ReportRequestDTO?> GetReportByIdAsync(Guid uuid);
        Task UpdateReportStatus(Guid uuid, RaporDurumu durum);
    }
}