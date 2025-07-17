using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Contracts;
using DomainLayer.Models.Identity_Module;
using DomainLayer.Models.Request_Module;
using DomainLayer.Models.Trip_Module;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Persistence.Repositories
{
    public class PatientRepository:GenericRepository<Patient>, IPatientRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public PatientRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Patient?> GetPatientByUserIdAsync(string userId)
        {
            return await _dbContext.Patients
                .Include(p => p.User)
                .FirstOrDefaultAsync(p => p.User.Id == userId);
        }

        public async Task<IEnumerable<Request>> GetRequestsByPatientIdAsync(int patientId)
        {
            return await _dbContext.Requests
                .Where(r => r.PatientId == patientId)
                .Include(r => r.Driver).ThenInclude(d => d.User)
                .Include(r => r.Nurse).ThenInclude(n => n.User)
                .ToListAsync();
        }

        public async Task<IEnumerable<Trip>> GetTripsByPatientIdAsync(int patientId)
        {
            return await _dbContext.Trips
                .Include(t => t.Request)
                .Where(t => t.Request.PatientId == patientId)
                .Include(t => t.Driver).ThenInclude(d => d.User)
                .Include(t => t.Nurse).ThenInclude(n => n.User)
                .Include(t => t.Ambulance)
                .ToListAsync();
        }
    }
}
