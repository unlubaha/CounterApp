using Counter.Entities;
using Counter.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Counter.ReportService.Services
{
    public interface IReportService
    {
        Task<string> CreateReportAsync(string seriNumarasi);
        Task<IEnumerable<ReportDTO>> GetReportsAsync();
        Task<ReportDTO?> GetReportByIdAsync(Guid uuid);
    }
}