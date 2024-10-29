using Counter.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;
using Counter.ReportService.Services;

namespace Counter.ReportService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet("Reports")]
        public async Task<IActionResult> GetReports()
        {
            var reports = await _reportService.GetReportsAsync();
            return Ok(reports);
        }

        [HttpGet("Reports/{uuid}")]
        public async Task<IActionResult> GetReportById(Guid uuid)
        {
            var report = await _reportService.GetReportByIdAsync(uuid);
            return report != null ? Ok(report) : NotFound("Rapor bulunamadı.");
        }
    }
}