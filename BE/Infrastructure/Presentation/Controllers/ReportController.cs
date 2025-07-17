using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using System.Threading.Tasks;

namespace Presentation.Controllers
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

        [HttpGet("totals")]
        public async Task<IActionResult> GetTotals()
        {
            var totalRequests = await _reportService.GetTotalRequestsAsync();
            var totalTrips = await _reportService.GetTotalTripsAsync();
            var totalRevenue = await _reportService.GetTotalRevenueAsync();
            return Ok(new { totalRequests, totalTrips, totalRevenue });
        }

        [HttpGet("most-active-drivers")]
        public async Task<IActionResult> GetMostActiveDrivers()
        {
            var drivers = await _reportService.GetMostActiveDriversAsync();
            return Ok(drivers);
        }

        [HttpGet("most-active-nurses")]
        public async Task<IActionResult> GetMostActiveNurses()
        {
            var nurses = await _reportService.GetMostActiveNursesAsync();
            return Ok(nurses);
        }
    }
}