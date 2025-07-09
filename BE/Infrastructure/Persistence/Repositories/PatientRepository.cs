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
    }
}
