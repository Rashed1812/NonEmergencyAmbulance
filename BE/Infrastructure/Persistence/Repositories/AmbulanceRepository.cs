using DomainLayer.Contracts;
using DomainLayer.Models.Ambulance_Module;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class AmbulanceRepository : GenericRepository<Ambulance>, IAmbulanceRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public AmbulanceRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Ambulance>> GetAllWithRelatedData()
        {
            return await _dbContext.Ambulances.Include(a => a.Driver).ThenInclude(d => d.User).ToListAsync();
        }

        public async Task<Ambulance> GetByIdWithRelatedData(int id)
        {
            return await _dbContext.Ambulances.Include(a => a.Driver).ThenInclude(d => d.User).FirstOrDefaultAsync(a => a.AmbulanceId == id);
        }

        public async Task AssignDriverAsync(int ambulanceId, int driverId)
        {
            var ambulance = await _dbContext.Ambulances.FindAsync(ambulanceId);
            if (ambulance != null)
            {
                ambulance.DriverId = driverId;
                await _dbContext.SaveChangesAsync();
            }
        }
    }
} 