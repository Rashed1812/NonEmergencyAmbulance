using DomainLayer.Contracts;
using Persistence.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class ReportRepository : IReportRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public ReportRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<int> GetTotalRequestsAsync()
        {
            return await _dbContext.Requests.CountAsync();
        }
        public async Task<int> GetTotalTripsAsync()
        {
            return await _dbContext.Trips.CountAsync();
        }
        public async Task<decimal> GetTotalRevenueAsync()
        {
            return await _dbContext.Trips.SumAsync(t => (decimal?)t.Price) ?? 0;
        }
        public async Task<IEnumerable<(int DriverId, int TripCount)>> GetMostActiveDriversAsync(int top = 5)
        {
            var data = await _dbContext.Trips
                .Where(t => t.DriverId != null)
                .GroupBy(t => t.DriverId)
                .Select(g => new IdCountDto { Id = g.Key, Count = g.Count() })
                .OrderByDescending(x => x.Count)
                .Take(top)
                .ToListAsync();
            return data.Select(x => (x.Id, x.Count));
        }

        public async Task<IEnumerable<(int NurseId, int TripCount)>> GetMostActiveNursesAsync(int top = 5)
        {
            var data = await _dbContext.Trips
                .Where(t => t.NurseId != null)
                .GroupBy(t => t.NurseId)
                .Select(g => new IdCountDto { Id = g.Key.Value, Count = g.Count() })
                .OrderByDescending(x => x.Count)
                .Take(top)
                .ToListAsync();
            return data.Select(x => (x.Id, x.Count));
        }
        private class IdCountDto
        {
            public int Id { get; set; }
            public int Count { get; set; }
        }
    }

} 