using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Models.Trip_Module;

namespace DomainLayer.Contracts
{
    public interface ITripRepository : IGenericRepository<Trip>
    {
        Task<IEnumerable<Trip>> GetAllWithRelatedData();
        Task<Trip> GetByIdWithRelatedData(int id);
        Task<IEnumerable<Trip>> GetByDriverIdAsync(int driverId);
        Task<IEnumerable<Trip>> GetByNurseIdAsync(int nurseId);
        Task<Trip?> GetByRequestIdAsync(int requestId);
    }
}
