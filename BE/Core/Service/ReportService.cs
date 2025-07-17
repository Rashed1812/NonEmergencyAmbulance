using DomainLayer.Contracts;
using ServiceAbstraction;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service
{
    public class ReportService : IReportService
    {
        private readonly IReportRepository _reportRepository;
        public ReportService(IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }
        public Task<int> GetTotalRequestsAsync() => _reportRepository.GetTotalRequestsAsync();
        public Task<int> GetTotalTripsAsync() => _reportRepository.GetTotalTripsAsync();
        public Task<decimal> GetTotalRevenueAsync() => _reportRepository.GetTotalRevenueAsync();
        public Task<IEnumerable<(int DriverId, int TripCount)>> GetMostActiveDriversAsync(int top = 5) => _reportRepository.GetMostActiveDriversAsync(top);
        public Task<IEnumerable<(int NurseId, int TripCount)>> GetMostActiveNursesAsync(int top = 5) => _reportRepository.GetMostActiveNursesAsync(top);
    }
} 