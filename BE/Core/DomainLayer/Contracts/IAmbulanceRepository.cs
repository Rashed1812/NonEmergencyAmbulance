using DomainLayer.Models.Ambulance_Module;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DomainLayer.Contracts
{
    public interface IAmbulanceRepository : IGenericRepository<Ambulance>
    {
        Task<IEnumerable<Ambulance>> GetAllWithRelatedData();
        Task<Ambulance> GetByIdWithRelatedData(int id);
        Task AssignDriverAsync(int ambulanceId, int driverId);
    }
} 