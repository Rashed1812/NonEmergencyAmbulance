using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Contracts;
using DomainLayer.Models.Identity_Module;
using Persistence.Data;

namespace Persistence.Repositories
{
    public class NurseRepository :GenericRepository<Nurse> , INurseRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public NurseRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<IEnumerable<Nurse>> GetAvailableNursesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
