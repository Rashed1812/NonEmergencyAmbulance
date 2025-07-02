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
    public class PatientRepository:GenericRepository<Patient>, IPatientRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public PatientRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
