using System.Threading.Tasks;
using System.Collections.Generic;

namespace DomainLayer.Contracts
{
    public interface IReportRepository
    {
        Task<int> GetTotalRequestsAsync();
        Task<int> GetTotalTripsAsync();
        Task<decimal> GetTotalRevenueAsync();
        Task<IEnumerable<(int DriverId, int TripCount)>> GetMostActiveDriversAsync(int top = 5);
        Task<IEnumerable<(int NurseId, int TripCount)>> GetMostActiveNursesAsync(int top = 5);
    }
} 