using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Models.Identity_Module;

namespace DomainLayer.Contracts
{
    public interface INurseRepository : IGenericRepository<Nurse>
    {
        Task<IEnumerable<Nurse>> GetAvailableNursesAsync();
    }
}
