using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Core;
using DomainLayer.Contracts;
using DomainLayer.Models.Identity_Module;
using DomainLayer.Models.Trip_Module;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Persistence.Repositories
{
    public class NurseRepository : GenericRepository<Nurse>, INurseRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public NurseRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Nurse>> GetAllWithRelatedData()
        {
            return await _dbContext.Nurses.Include(n => n.User).ToListAsync();
        }

        public async Task<Nurse> GetByIdWithRelatedData(int id)
        {
            var nurse = await _dbContext.Nurses
                .Include(n => n.User)
                .FirstOrDefaultAsync(n => n.Id == id);

            return nurse;
        }

        public async Task<IEnumerable<Nurse>> GetAvailableAsync()
        {
            return await _dbContext.Nurses.Include(n => n.User).Where(n => n.IsAvailable).ToListAsync();
        }

  

    }
}
