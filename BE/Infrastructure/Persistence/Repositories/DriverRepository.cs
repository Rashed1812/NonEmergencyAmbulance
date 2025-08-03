using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Contracts;
using DomainLayer.Models.Identity_Module;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Persistence.Repositories
{
    public class DriverRepository : GenericRepository<Driver>, IDriverRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public DriverRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Driver>> GetAllWithRelatedData()
        {
            return await _dbContext.Drivers
                .Include(d => d.User)
                .ToListAsync();
        }

        public async Task<Driver> GetByIdWithRelatedData(int id)
        {
            var driver = await _dbContext.Drivers
                .Include(d => d.User)
                .FirstOrDefaultAsync(d => d.Id == id);

            return driver;
        }

        public async Task<Driver> GetByIdWithRequestsAndTripsAsync(int driverId)
        {
            return await _dbContext.Drivers
                .Include(d => d.AssignedRequests)
                .Include(d => d.Trips)
                    .ThenInclude(t => t.Request)
                .Include(d => d.Ambulances)
                .FirstOrDefaultAsync(d => d.Id == driverId);
        }

        public async Task<Driver> GetDriverByIdWithAmbulance(int id)
        {
            return await _dbContext.Drivers
                .Include(d => d.Ambulances)
                .FirstOrDefaultAsync(d => d.Id == id);
        }
    }
}
