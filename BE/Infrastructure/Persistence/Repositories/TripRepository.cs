using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Contracts;
using DomainLayer.Models.Trip_Module;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Persistence.Repositories
{
    public class TripRepository : GenericRepository<Trip>, ITripRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public TripRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
            
        }
        public async Task<IEnumerable<Trip>> GetAllWithRelatedData()
        {
            return await _dbContext.Trips
                    .Include(t => t.Driver)
                    .ThenInclude(d => d.User)
                    .Include(t => t.Nurse)
                    .ThenInclude(n => n.User)
                    .Include(t => t.Ambulance)
                    .Include(t => t.Request)
                    .ToListAsync();
        }
        public async Task<Trip> GetByIdWithRelatedData(int id)
        {
            return await _dbContext.Trips
                .Include(t => t.Driver)
                .ThenInclude(d => d.User)
                .Include(t => t.Nurse)
                .ThenInclude(n => n.User)
                .Include(t => t.Ambulance)
                .Include(t => t.Request)
                .FirstOrDefaultAsync(t => t.TripId == id);
        }

        public async Task<IEnumerable<Trip>> GetByDriverIdAsync(int driverId)
        {
            return await _dbContext.Trips
                .Where(t => t.DriverId == driverId)
                .Include(t => t.Driver).ThenInclude(d => d.User)
                .Include(t => t.Nurse).ThenInclude(n => n.User)
                .Include(t => t.Request)
                .ToListAsync();
        }


        public async Task<IEnumerable<Trip>> GetByNurseIdAsync(int nurseId)
        {
            return await _dbContext.Trips
                .Where(t => t.NurseId == nurseId)
                .Include(t => t.Driver).ThenInclude(d => d.User)
                .Include(t => t.Nurse).ThenInclude(n => n.User)
                .Include(t => t.Request)
                .ToListAsync();
        }

        public async Task<Trip?> GetByRequestIdAsync(int requestId)
        {
            return await _dbContext.Trips
                .Include(t => t.Driver).ThenInclude(d => d.User)
                .Include(t => t.Nurse).ThenInclude(n => n.User)
                .Include(t => t.Request)
                .Include(t => t.Ambulance)
                .FirstOrDefaultAsync(t => t.RequestId == requestId);
        }
    }
}
